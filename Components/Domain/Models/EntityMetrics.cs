using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dainiki.Components.Domain.Models
{
    public class EntityMetrics
    {
        public int TotalEntries { get; set; }
        public int EntriesThisWeek { get; set; }
        public int TotalEntriesThisMonth { get;set;}
        public int CurrentStreakDays { get; set; }
        public int LongestStreakDays { get; set; }
    }
}
