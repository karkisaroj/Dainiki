using Dainiki.Components.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dainiki.Components.Shared.Utils
{
    public class EntityMetricsCalculator
    {
        public static EntityMetrics Calculate(List<EntriesModel> entries)
        {
            if (entries == null || entries.Count == 0)
                return new EntityMetrics();

            var latestDate = entries.Max(e => e.CreatedAt.Date);
            var startOfWeek = latestDate.AddDays(-(int)latestDate.DayOfWeek); // Sunday as first day
            var startOfMonth = new DateTime(latestDate.Year, latestDate.Month, 1);

            var metrics = new EntityMetrics
            {
                TotalEntries = entries.Count,
                EntriesThisWeek = entries.Count(e => e.CreatedAt.Date >= startOfWeek && e.CreatedAt.Date <= latestDate),
                TotalEntriesThisMonth = entries.Count(e => e.CreatedAt.Date >= startOfMonth && e.CreatedAt.Date <= latestDate),
                CurrentStreakDays = CalculateCurrentStreak(entries),
                LongestStreakDays = CalculateLongestStreak(entries)
            };

            return metrics;
        }

        /// <summary>
        /// Calculates the current streak ending at the latest entry.
        /// Resets if a day is missed.
        /// </summary>
        private static int CalculateCurrentStreak(List<EntriesModel> entries)
        {
            if (entries == null || entries.Count == 0) return 0;

            var entryDates = entries
                .Select(e => e.CreatedAt.Date)
                .Distinct()
                .ToHashSet();

            int streak = 0;
            var current = DateTime.Today; // this enforce streak relative to today

            // Count backwards from today until a gap is found
            while (entryDates.Contains(current))
            {
                streak++;
                current = current.AddDays(-1);
            }

            return streak;
        }

        /// <summary>
        /// Calculates the longest streak across all entries.
        /// </summary>
        private static int CalculateLongestStreak(List<EntriesModel> entries)
        {
            var entryDates = entries
                .Select(e => e.CreatedAt.Date)
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            int longest = 0, current = 0;
            DateTime? prev = null;

            foreach (var date in entryDates)
            {
                if (prev == null || date == prev.Value.AddDays(1))
                {
                    current++;
                    if (current > longest) longest = current;
                }
                else
                {
                    current = 1; // reset streak when gap found
                }
                prev = date;
            }

            return longest;
        }
    }
}