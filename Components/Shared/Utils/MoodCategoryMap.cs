using System.Collections.Generic;

namespace Dainiki.Components.Shared.Utils
{
    public static class MoodCategoryMap
    {
        public static readonly Dictionary<string, string> MoodToCategory = new()
        {
            { "Happy", "Positive" },
            { "Excited", "Positive" },
            { "Sad", "Negative" },
            { "Angry", "Negative" },
            { "Calm", "Neutral" }
        };  

        public static string GetCategory(string mood)
        {
            return MoodToCategory.TryGetValue(mood, out var category) ? category : "Neutral";
        }
    }
}