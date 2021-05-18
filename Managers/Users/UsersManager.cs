using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EatingDaily.Storage;
using EatingDaily.Storage.Entity;

namespace ToDoBook.Managers.Users
{
	public class UsersManager : IUsersManager
	{
		WorkContext _context;
		public UsersManager(WorkContext context)
		{
			_context = context;
		}

		public void DelUser(int i)
		{
			var RemoveUser = _context.Users.FirstOrDefault(user => user.ID == i);
			if (RemoveUser != null)
				_context.Remove(RemoveUser);
		}

		public List<User> GetAll()
		{
			return _context.Users.ToList();
		}

		public User GetIn(int i)
		{
			return _context.Users.FirstOrDefault(user => user.ID == i);
		}
	}
}
