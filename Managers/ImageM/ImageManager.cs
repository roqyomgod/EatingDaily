using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EatingDaily.Storage;
using EatingDaily.Storage.Entity;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using EatingDaily.Managers.Profiles;

namespace ToDoBook.Managers.ImageM
{
	public class ImageManager
	{
		WorkContext _context;
		IWebHostEnvironment _appEnvironment;

		public ImageManager(WorkContext context)
		{
			_context = context;
		}

		public void AddAvatar(IFormFile pvm, int UserID)
		{
			ImageEntry person = new ImageEntry();
			ProfileManager profile = new ProfileManager(_context);

			if (pvm != null)
			{
				byte[] imageData = null;
				using (var binaryReader = new BinaryReader(pvm.OpenReadStream()))
				{
					imageData = binaryReader.ReadBytes((int)pvm.Length);
				}
				person.Image = imageData;
			}

			int id = profile.GetIn(UserID).ImageID;
			if (id != 0)
				_context.Images.FirstOrDefault(Im => Im.ID == id).Image = person.Image;
			else
			{
				_context.Images.Add(person);
				_context.SaveChanges();
				_context.Profiles.FirstOrDefault(profile => profile.ID == UserID).ImageID = _context.Images.ToList().Last().ID;
			}
			_context.SaveChanges();
		}
	}
}

