using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EatingDaily.Storage.Entity
{
    public class Profile
    {
		[Key]
		public int ID { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public int Age { get; set; }
		public int ImageID { get; set; }
	}
}
