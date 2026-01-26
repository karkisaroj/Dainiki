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
                
            };

            return metrics;
        }

    }
}