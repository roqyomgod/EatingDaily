using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EatingDaily.Storage.Entity;

namespace EatingDaily.Managers.Profiles
{
	interface IProfileManager
	{
		List<Profile> GetAll();
		Profile GetIn(int i);
	}
}
