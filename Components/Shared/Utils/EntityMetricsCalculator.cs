using Dainiki.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dainiki.Components.Utils
{
    public class EntityMetricsCalculator
    {
        public static EntityMetrics Calculate(List<EntriesModel> entries)
        {
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            var metrics = new EntityMetrics
            {
                TotalEntries = entries.Count,
                EntriesThisWeek = entries.Count(e => e.CreatedAt.Date >= startOfMonth),
                TotalEntriesThisMonth = entries.Count(e => e.CreatedAt.Date >= startOfMonth),
                CurrentStreakDays = CalculateStreak(entries)
            };
            return metrics;
        }

        private static int CalculateStreak(List<EntriesModel> entries)
        {
            var dates = entries.Select(e => e.CreatedAt).Distinct().OrderByDescending(d => d).ToList();
            int streak = 0;
            var current = DateTime.Today;

            foreach (var date in dates)
            {
                if (date == current)
                {
                    streak++;
                    current = current.AddDays(-1);
                }
                else
                {
                    break;
                }
            }
            return streak;
        }
    }
}
