using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dainiki.Components.Models
{
    public class AnalyticsViewModel
    {
        public List<MoodInfo> MoodData { get; set; }
        public List<EntryInfo> EntryData { get; set; }
        public List<TagInfo> TagData { get; set; }
        public List<CategoryInfo> CategoryData { get; set; }

        public AnalyticsViewModel()
        {
            MoodData = new()
        {
            new MoodInfo { Mood = "Positive", Value = 65 },
            new MoodInfo { Mood = "Neutral", Value = 20 },
            new MoodInfo { Mood = "Negative", Value = 15 }
        };

            EntryData = new()
        {
            new EntryInfo { Date = "Jan 8", Words = 300 },
            new EntryInfo { Date = "Jan 9", Words = 420 },
            new EntryInfo { Date = "Jan 10", Words = 390 },
            new EntryInfo { Date = "Jan 11", Words = 500 },
            new EntryInfo { Date = "Jan 12", Words = 600 },
            new EntryInfo { Date = "Jan 13", Words = 450 },
            new EntryInfo { Date = "Jan 14", Words = 470 },
            new EntryInfo { Date = "Jan 15", Words = 520 }
        };

            TagData = new()
        {
            new TagInfo { Tag = "Work", Count = 28 },
            new TagInfo { Tag = "Health", Count = 26 },
            new TagInfo { Tag = "Family", Count = 24 },
            new TagInfo { Tag = "Travel", Count = 23 },
            new TagInfo { Tag = "Study", Count = 22 },
            new TagInfo { Tag = "Finance", Count = 21 }
        };

            CategoryData = new()
        {
            new CategoryInfo { Category = "Personal", Count = 36 },
            new CategoryInfo { Category = "Professional", Count = 27 },
            new CategoryInfo { Category = "Wellness", Count = 22 },
            new CategoryInfo { Category = "Other", Count = 15 }
        };
        }
    }

    public class MoodInfo { public string Mood { get; set; } public double Value { get; set; } }
    public class EntryInfo { public string Date { get; set; } public double Words { get; set; } }
    public class TagInfo { public string Tag { get; set; } public double Count { get; set; } }
    public class CategoryInfo { public string Category { get; set; } public double Count { get; set; } }
}
