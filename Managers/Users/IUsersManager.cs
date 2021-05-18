using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EatingDaily.Storage.Entity;

namespace ToDoBook.Managers.Users
{
	interface IUsersManager
	{
		List<User> GetAll();
		User GetIn(int i);
		void DelUser(int i);
	}
}
