using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EatingDaily.Storage.Entity
{
    public class ChecklistEntry : Entry
    {
        public string Contents { get; set; }
        public string TrueFalses { get; set; }
        public int Positions { get; set; }

        public ChecklistEntry()
        {
            Contents = TrueFalses = "";
            Positions = 0;
        }

        public ChecklistEntry(List<Check> checks)
        {
            Set(checks);
        }

        public void Set(List<Check> checks)
        {
            Positions = 0;
            Contents = "";
            Positions = 0;
            TrueFalses = "";
            foreach (var check in checks)
            {
                Contents += check.Name + '\n';
                TrueFalses += check.State ? '1' : '0';
                Positions++;
            }
        }

        public List<Check> ToList()
        {
            List<Check> checks = new List<Check>();
            string[] names = Contents.Split('\n');
            for (var i = 0; i < Positions; i++)
            {
                Check check = new Check(names[i], TrueFalses[i] == '0' ? false : true);
                checks.Add(check);
            }
            return checks;
        }
    }

    public class Check
    {
        public string Name { get; set; }
        public bool State { get; set; }
        public Check(string name, bool state)
        {
            Name = name;
            State = state;
        }
        public Check()
        { }
    }
}
