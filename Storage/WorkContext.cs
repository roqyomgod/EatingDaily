using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EatingDaily.Storage.Entity;
using EatingDaily.Storage.StorageEntity;

namespace EatingDaily.Storage
{
    public class WorkContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Diet> Diets { get; set; }
        public DbSet<Belonging> Belongings { get; set; }
        public DbSet<EntriesBelonging> Entries { get; set; }
        public DbSet<TextEntry> TextEntries { get; set; }
        public DbSet<ReminderEntry> ReminderEntries { get; set; }
        public DbSet<TimerEntry> TimerEntries { get; set; }
        public DbSet<ChecklistEntry> ChecklistEntries { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Source> SourceData { get; set; }
        public DbSet<ImageEntry> Images { get; set; }

        public WorkContext(DbContextOptions<WorkContext> options) : base(options)
        {
          
            Database.EnsureCreated();
        }
    }
}
