using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EatingDaily.Storage.Entity
{
    public class EntriesBelonging
    {
        [Key]
        public int BelongingID { get; set; }
        public int DietID { get; set; }
        public int Type { get; set; }
        public int EntryID { get; set; }
    }
}
