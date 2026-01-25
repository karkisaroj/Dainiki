using System;
using System.Collections.Generic;
using System.Linq;

namespace Dainiki.Components.Models
{
    public class AnalyticsModel
    {
        public List<MoodInfo> MoodData { get; set; }
        public List<EntryInfo> EntryData { get; set; }
        public List<TagInfo> TagData { get; set; }
        public List<CategoryInfo> CategoryData { get; set; }

        public double AvgWordCount => EntryData.Any() ? EntryData.Average(e => e.Words) : 0;
        public string TopTag => TagData.OrderByDescending(t => t.Count).FirstOrDefault()?.Tag ?? string.Empty;
        public int CurrentStreak => CalculateCurrentStreak();
        public int LongestStreak => CalculateLongestStreak();
        public int MissedDays => EntryData.Count(e => e.Words == 0);
        public string MostFrequentMood => MoodData.OrderByDescending(m => m.Value).FirstOrDefault()?.Mood ?? string.Empty;

        public AnalyticsModel()
        {
            MoodData = new List<MoodInfo>();
            EntryData = new List<EntryInfo>();
            TagData = new List<TagInfo>();
            CategoryData = new List<CategoryInfo>();
        }

        private int CalculateCurrentStreak()
        {
            int streak = 0;
            foreach (var entry in EntryData.OrderByDescending(e => DateTime.Parse(e.Date)))
            {
                if (entry.Words > 0) streak++;
                else break;
            }
            return streak;
        }

        private int CalculateLongestStreak()
        {
            int longest = 0, current = 0;
            foreach (var entry in EntryData.OrderBy(e => DateTime.Parse(e.Date)))
            {
                if (entry.Words > 0)
                {
                    current++;
                    if (current > longest) longest = current;
                }
                else current = 0;
            }
            return longest;
        }
    }

    public class MoodInfo
    {
        public string Mood { get; set; } = string.Empty;
        public double Value { get; set; } = 0;
    }

    public class EntryInfo
    {
        public string Date { get; set; } = string.Empty;
        public double Words { get; set; } = 0;
    }

    public class TagInfo
    {
        public string Tag { get; set; } = string.Empty;
        public double Count { get; set; } = 0;
    }

    public class CategoryInfo
    {
        public string Category { get; set; } = string.Empty;
        public double Count { get; set; } = 0;
    }
}