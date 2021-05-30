using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EatingDaily.Storage;
using EatingDaily.Storage.Entity;
using EatingDaily.Managers.Profiles;
using EatingDaily.Managers.SourceM;
using EatingDaily.Storage.StorageEntity;
using EatingDaily.Managers.DietM;
using EatingDaily.Managers.EntryM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ToDoBook.Managers.ImageM;

namespace EatingDaily.Controllers
{
	[Authorize]
	public class UserController : Controller
	{
		WorkContext _context;

		public UserController(WorkContext context, IWebHostEnvironment appEnvironment)
		{
			_context = context;
		}
		[HttpGet]
		public ActionResult Index()
		{
			ProfileManager profileManager = new ProfileManager(_context);
			Profile profile = profileManager.GetIn(int.Parse(User.Identity.Name));
			ViewBag.Profile = profile;
			if (profile.ImageID != 0)
				ViewBag.Avatar = _context.Images.FirstOrDefault(imeg => imeg.ID == profile.ImageID).Image;
			return View();
		}
		//
		[HttpPost]
		public ActionResult Index(Profile data, IFormFile Avatar)
		{
			if (Avatar != null)
			{
				ImageManager AddImage = new ImageManager(_context);
				AddImage.AddAvatar(Avatar, int.Parse(User.Identity.Name));
				data.ImageID = _context.Profiles.Find(data.ID).ImageID;
				ViewBag.Avatar = _context.Images.FirstOrDefault(imeg => imeg.ID == data.ImageID).Image;
			}
			_context.Profiles.Remove(_context.Profiles.Find(data.ID));
			_context.Profiles.Add(data);
			_context.SaveChanges();
			ViewBag.Profile = data;
			return View(data);
		}

		public ActionResult News()
		{
			SourceManager News_data = new SourceManager(_context);
			return View(News_data.GetNews());
		}

		[HttpGet]
		public ActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Add(User data)
		{
			_context.Users.Add(data);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult AddDiet()
		{
			return View();
		}

		[HttpPost]
		public ActionResult AddDiet(Diet data)
		{
			DietManager diet = new DietManager(_context);
			diet.AddDiet(data, int.Parse(User.Identity.Name));

			return RedirectToAction("RequestDiet");
		}

		public ActionResult RequestDiet(int? ID)
		{
			DietManager diet = new DietManager(_context);
			return View(diet.GetDiets(ID ?? int.Parse(User.Identity.Name)));
		}

		public IActionResult Show_Entry(int ID)
		{
			EntryManager entry = new EntryManager(_context);
			ViewBag.ID = ID;
			return View(entry.GetEntries(ID));
		}

		[HttpGet]
		public IActionResult Edit_Text_Entry(int id, int id2)
		{
			ViewBag.ID = id2;
			ViewBag.Entries = _context.TextEntries.Find(id);
			return View();
		}

		[HttpPost]
		public ActionResult Edit_Text_Entry(TextEntry data, int DietID)
		{
			_context.TextEntries.Find(data.ID).Text = data.Text;
			_context.TextEntries.Find(data.ID).Name = data.Name;
			_context.TextEntries.Find(data.ID).Description = data.Description;
			_context.SaveChanges();
			return RedirectToAction("Show_Entry", new { ID = DietID });
		}

		[HttpGet]
		public IActionResult Edit_Image_Entry(int id, int id2)
		{
			ImageEntry imageEntry = _context.Images.Find(id);
			ViewBag.ID = id2;
			ViewBag.Entries = imageEntry;
			if (imageEntry.Image != null)
				ViewBag.Image = imageEntry.Image;
			return View();
		}

		[HttpPost]
		public ActionResult Edit_Image_Entry(TextEntry data, int DietID, IFormFile pvm)
		{
			_context.Images.Find(data.ID).Name = data.Name;
			_context.Images.Find(data.ID).Description = data.Description;
			if (pvm != null)
			{
				byte[] imageData = null;
				using (var binaryReader = new BinaryReader(pvm.OpenReadStream()))
				{
					imageData = binaryReader.ReadBytes((int)pvm.Length);
				}
				_context.Images.Find(data.ID).Image = imageData;
			}
			_context.SaveChanges();
			return RedirectToAction("Show_Entry", new { ID = DietID });
		}

		[HttpGet]
		public IActionResult Edit_Reminder_Entry(int id, int id2)
		{
			ViewBag.ID = id2;
			ViewBag.Entries = _context.ReminderEntries.Find(id);
			return View();
		}

		[HttpPost]
		public ActionResult Edit_Reminder_Entry(ReminderEntry data, int DietID)
		{
			_context.ReminderEntries.Find(data.ID).Name = data.Name;
			_context.ReminderEntries.Find(data.ID).Description = data.Description;
			_context.ReminderEntries.Find(data.ID).Time = data.Time;
			_context.ReminderEntries.Find(data.ID).Email = data.Email;
			_context.SaveChanges();
			return RedirectToAction("Show_Entry", new { ID = DietID });
		}

		[HttpGet]
		public IActionResult Edit_Timer_Entry(int id, int id2)
		{
			ViewBag.ID = id2;
			ViewBag.Entries = _context.TimerEntries.Find(id);
			return View();
		}

		[HttpPost]
		public ActionResult Edit_Timer_Entry(TimerEntry data, int DietID)
		{
			_context.TimerEntries.Find(data.ID).Name = data.Name;
			_context.TimerEntries.Find(data.ID).Description = data.Description;
			_context.TimerEntries.Find(data.ID).EndTime = data.EndTime;
			_context.SaveChanges();
			return RedirectToAction("Show_Entry", new { ID = DietID });
		}

		[HttpGet]
		public IActionResult Edit_Checklist_Entry(int id, int id2)
		{
			ChecklistEntry checklist = _context.ChecklistEntries.Find(id);

			ViewBag.ID = id2;
			ViewBag.Entries = checklist;
			return View(checklist.ToList());
		}

		[HttpPost]
		public ActionResult Edit_Checklist_Entry(List<Check> model, ChecklistEntry data, int DietID, string action)
		{
			if (action == "Отправить")
			{
				_context.ChecklistEntries.Find(data.ID).Name = data.Name;
				_context.ChecklistEntries.Find(data.ID).Description = data.Description;
				_context.ChecklistEntries.Find(data.ID).Set(model);
				_context.SaveChanges();
				return RedirectToAction("Show_Entry", new { ID = DietID });
			}
			else if (action == "Добавить чек")
			{
				{
					_context.ChecklistEntries.Find(data.ID).Name = data.Name;
					_context.ChecklistEntries.Find(data.ID).Description = data.Description;
					_context.ChecklistEntries.Find(data.ID).Set(model);
					model.Add(new Check());
					_context.ChecklistEntries.Find(data.ID).Set(model);
					_context.SaveChanges();
					return RedirectToAction("Edit_Checklist_Entry", new { id = data.ID, id2 = DietID });
				}
			}
			else
			{
				_context.ChecklistEntries.Find(data.ID).Name = data.Name;
				_context.ChecklistEntries.Find(data.ID).Description = data.Description;
				_context.ChecklistEntries.Find(data.ID).Set(model);
				model.Remove(model.Last());
				_context.ChecklistEntries.Find(data.ID).Set(model);
				_context.SaveChanges();
				return RedirectToAction("Edit_Checklist_Entry", new { id = data.ID, id2 = DietID });
			}
		}

		[HttpGet]
		public ActionResult AddTextEntry(int ID)
		{
			ViewBag.ID = ID;
			return View();
		}

		[HttpPost]
		public ActionResult AddTextEntry(TextEntry data, int DietID)
		{
			EntryManager entry = new EntryManager(_context);
			data.Type = "Edit_Text_Entry";
			data.ID = 0;
			entry.AddEntry(data, DietID);
			return RedirectToAction("Show_Entry", new { ID = DietID });
		}

		[HttpGet]

		public ActionResult AddImageEntry(int ID)
		{
			ViewBag.ID = ID;
			return View();
		}

		[HttpPost]
		public ActionResult AddImageEntry(ImageEntry data, int DietID)
		{
			EntryManager entry = new EntryManager(_context);
			data.Type = "Edit_Image_Entry";
			data.ID = 0;
			entry.AddEntry(data, DietID);
			return RedirectToAction("Show_Entry", new { ID = DietID });
		}

		[HttpGet]
		public ActionResult AddReminderEntry(int ID)
		{
			ViewBag.ID = ID;
			return View();
		}

		[HttpPost]
		public ActionResult AddReminderEntry(ReminderEntry data, int DietID)
		{
			EntryManager entry = new EntryManager(_context);
			data.ID = 0;
			entry.AddEntry(data, DietID);
			return RedirectToAction("Show_Entry", new { ID = DietID });
		}

		[HttpGet]
		public ActionResult AddTimerEntry(int ID)
		{
			ViewBag.ID = ID;
			return View();
		}

		[HttpPost]
		public ActionResult AddTimerEntry(TimerEntry data, int DietID)
		{
			EntryManager entry = new EntryManager(_context);
			data.ID = 0;
			entry.AddEntry(data, DietID);
			return RedirectToAction("Show_Entry", new { ID = DietID });
		}

		[HttpGet]
		public ActionResult AddChecklistEntry(int ID)
		{
			ViewBag.ID = ID;
			return View();
		}

		[HttpPost]
		public ActionResult AddChecklistEntry(ChecklistEntry data, int DietID)
		{
			EntryManager entry = new EntryManager(_context);
			data.ID = 0;
			entry.AddEntry(data, DietID);
			return RedirectToAction("Show_Entry", new { ID = DietID });
		}

		public ActionResult DeleteDiet(int ID)
		{
			DietManager dietManager = new DietManager(_context);

			dietManager.DeleteDiets(ID);
			return RedirectToAction("RequestDiet");
		}

		public ActionResult DeleteEntry(int ID, int id2)
		{
			EntryManager entryManager = new EntryManager(_context);

			entryManager.DeleteEntry(ID);
			return RedirectToAction("Show_Entry", new { ID = id2 });
		}

	}
}
