using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EatingDaily.Storage.StorageEntity
{
    public class Belonging
    {
        [Key]
        public int BelongingID { get; set; }
        public int UserID { get; set; }
        public int DietID { get; set; }
    }
}
