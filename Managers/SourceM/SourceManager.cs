using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EatingDaily.Storage;
using EatingDaily.Storage.Entity;

namespace EatingDaily.Managers.SourceM
{
	public class SourceManager
	{
		WorkContext _context;
		public SourceManager(WorkContext context)
		{
			_context = context;
		}

		public List<Source> GetNews()
		{
			return _context.SourceData.ToList();
		}
	}
}
