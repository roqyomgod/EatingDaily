using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EatingDaily.Storage;
using EatingDaily.Storage.Entity;

namespace EatingDaily.Managers.Profiles
{
	public class ProfileManager : IProfileManager
	{

		WorkContext _context;

		public ProfileManager(WorkContext context)
		{
			_context = context;
		}

		public List<Profile> GetAll()
		{
			return _context.Profiles.ToList();
		}
		public Profile GetIn(int i)
		{
			return _context.Profiles.FirstOrDefault(profile => profile.ID == i);
		}
	}
}
