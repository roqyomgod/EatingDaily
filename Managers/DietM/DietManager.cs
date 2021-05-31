using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EatingDaily.Managers.EntryM;
using EatingDaily.Storage;
using EatingDaily.Storage.Entity;
using EatingDaily.Storage.StorageEntity;

namespace EatingDaily.Managers.DietM
{
    public class DietManager
    {
		WorkContext _context;

		public DietManager(WorkContext context)
		{
			_context = context;
		}

		public void AddDiet(Diet NewDiet, int Userid)
		{
			_context.Diets.Add(NewDiet);
			_context.SaveChanges();
			_context.Belongings.Add(new Belonging { UserID = Userid, DietID = _context.Diets.ToList().Last().ID });
			_context.SaveChanges();
		}

		public List<Diet> GetDiets(int id)
		{
			List<Diet> diets = new List<Diet>();

			foreach (var item in _context.Belongings.ToList().Where(x => x.UserID == id))
			{
				diets.Add(_context.Diets.Find(item.DietID));
			}
			return diets;
		}

		public void DeleteDiets(int ID)
		{
			EntryManager entryManager = new EntryManager(_context);

			entryManager.DelEntries(ID);
			_context = entryManager.GetContext();
			_context.Diets.Remove(_context.Diets.FirstOrDefault(diet => diet.ID == ID));
			_context.Belongings.Remove(_context.Belongings.FirstOrDefault(bel => bel.DietID == ID));
			foreach (var el in _context.Entries.Where(el => el.DietID == ID))
			{
				_context.Entries.Remove(el);
			}
			_context.SaveChanges();
		}

		public WorkContext GetContext() => _context;
	}
}

