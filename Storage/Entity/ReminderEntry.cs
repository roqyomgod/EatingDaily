using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatingDaily.Storage.Entity
{
    public class ReminderEntry : Entry
    {
        public DateTime Time { get; set; } = DateTime.Now;
        public string Email { get; set; }
    }
}