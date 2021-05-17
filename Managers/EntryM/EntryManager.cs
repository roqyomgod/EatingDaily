using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EatingDaily.Storage.Entity;
using EatingDaily.Storage;

namespace EatingDaily.Managers.EntryM
{
    public class EntryManager
    {
        WorkContext _context;

        public EntryManager(WorkContext context)
        {
            _context = context;
        }

        public List<Entry> GetEntries(int IdDiet)
        {
            List<Entry> entry = new List<Entry>();

            foreach (var item in _context.Entries.ToList().Where(x => x.DietID == IdDiet))
            {
                switch (item.Type)
                {
                    case 1:
                        entry.Add(_context.TextEntries.Find(item.EntryID));
                        break;
                    case 2:
                        //entry.Add(_context.MitingEntries.Find(item.EntryID));
                        break;
                    case 3:
                        entry.Add(_context.ReminderEntries.Find(item.EntryID));
                        break;
                    case 4:
                        entry.Add(_context.TimerEntries.Find(item.EntryID));
                        break;
                    case 6:
                        entry.Add(_context.ChecklistEntries.Find(item.EntryID));
                        break;
                    case 5:
                        entry.Add(_context.Images.Find(item.EntryID));
                        break;
                }
            }

            return entry;
        }

        public void DelEntries(int IdDiet)
        {

            foreach (var item in _context.Entries.ToList().Where(x => x.DietID == IdDiet))
            {
                switch (item.Type)
                {
                    case 1:
                        _context.TextEntries.Remove(_context.TextEntries.Find(item.EntryID));
                        break;
                    case 2:
                        //_context.MitingEntries.Remove(_context.MitingEntries.Find(item.EntryID));
                        break;
                    case 3:
                        _context.ReminderEntries.Remove(_context.ReminderEntries.Find(item.EntryID));
                        break;
                    case 4:
                        _context.TimerEntries.Remove(_context.TimerEntries.Find(item.EntryID));
                        break;
                    case 6:
                        _context.ChecklistEntries.Remove(_context.ChecklistEntries.Find(item.EntryID));
                        break;
                    case 5:
                        _context.Images.Remove(_context.Images.Find(item.EntryID));
                        break;
                }
            }
            _context.SaveChanges();
        }

        public void DeleteEntry(int ID)
        {
            EntriesBelonging deleted = _context.Entries.ToList().Find(x => x.EntryID == ID);
            switch (deleted.Type)
            {
                case 1:
                    _context.TextEntries.Remove(_context.TextEntries.Find(ID));
                    break;
                case 2:
                    //_context.MitingEntries.Remove(_context.MitingEntries.Find(ID));
                    break;
                case 3:
                    _context.ReminderEntries.Remove(_context.ReminderEntries.Find(ID));
                    break;
                case 4:
                    _context.TimerEntries.Remove(_context.TimerEntries.Find(ID));
                    break;
                case 6:
                    _context.ChecklistEntries.Remove(_context.ChecklistEntries.Find(ID));
                    break;
                case 5:
                    _context.Images.Remove(_context.Images.Find(ID));
                    break;
            }
            _context.Entries.Remove(deleted);
            _context.SaveChanges();
        }

        public void AddEntry(TextEntry entry, int IdDiet)
        {
            entry.Type = "Edit_Text_Entry";
            _context.TextEntries.Add(entry);
            _context.SaveChanges();
            _context.Entries.Add(new EntriesBelonging
            {
                Type = 1,
                EntryID = ///???
            _context.TextEntries.ToList().Last().ID,
                DietID = IdDiet
            });
            _context.SaveChanges();
        }

        //public void AddEntry(MitingEntry entry, int IdDiary)
        //{
        // entry.Type = "Edit_Miting_Entry";
        // _context.MitingEntries.Add(entry);
        // _context.SaveChanges();
        // _context.Entries.Add(new EntriesBelonging
        // {
        // Type = 2,
        // EntryID =
        // _context.MitingEntries.ToList().Last().ID,
        // DiaryID = IdDiary
        // });
        // _context.SaveChanges();
        //}

        public void AddEntry(ReminderEntry entry, int IdDiary)
        {
            entry.Type = "Edit_Reminder_Entry";
            _context.ReminderEntries.Add(entry);
            _context.SaveChanges();
            _context.Entries.Add(new EntriesBelonging
            {
                Type = 3,
                EntryID =
            _context.ReminderEntries.ToList().Last().ID,
                DietID = IdDiary
            });
            _context.SaveChanges();
        }

        public void AddEntry(TimerEntry entry, int IdDiary)
        {
            entry.Type = "Edit_Timer_Entry";
            _context.TimerEntries.Add(entry);
            _context.SaveChanges();

            _context.Entries.Add(new EntriesBelonging
            {
                Type = 4,
                EntryID =
            _context.TimerEntries.ToList().Last().ID,
                DietID = IdDiary
            });
            _context.SaveChanges();
        }

        public void AddEntry(ChecklistEntry entry, int IdDiary)
        {
            entry.Type = "Edit_Checklist_Entry";
            _context.ChecklistEntries.Add(entry);
            _context.SaveChanges();
            _context.Entries.Add(new EntriesBelonging
            {
                Type = 6,
                EntryID =
            _context.ChecklistEntries.ToList().Last().ID,
                DietID = IdDiary
            });
            _context.SaveChanges();
        }
        public void AddEntry(ImageEntry entry, int IdDiary)
        {
            entry.Type = "Edit_Image_Entry";
            _context.Images.Add(entry);
            _context.SaveChanges();
            _context.Entries.Add(new EntriesBelonging
            {
                Type = 5,
                EntryID =
            _context.Images.ToList().Last().ID,
                DietID = IdDiary
            });
            _context.SaveChanges();
        }

        public WorkContext GetContext() => _context;
    }
}