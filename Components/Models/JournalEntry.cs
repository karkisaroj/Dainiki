using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dainiki.Components.Models
{
    public class JournalEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Title { get; set; } = "";
        public string Mood { get; set; } = "";
        public string Preview { get; set; } = "";
    }
}
