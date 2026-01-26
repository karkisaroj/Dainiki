using Dainiki.Components.Database;
using Dainiki.Components.Domain.Models;
using Dainiki.Components.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dainiki.Components.Application.Services
{
    public class EntityService(JournalDatabase db)
    {
        public async Task<int> UpdateEntryAsync(EntriesModel entry)
        {
            entry.UpdatedAt = DateTime.Now;
            return await db.UpdateEntryAsync(entry);
        }

        public Task<List<EntriesModel>> GetEntriesByUserAsync(int userId)=> db.GetEntriesByUserAsync(userId);

        public async Task<EntriesModel?> GetEntryByDateAsync(int userId, DateTime date)
        {
            return await db.GetEntryByDateAsync(userId, date);
        }
        public Task<EntriesModel?> GetEntryByIdAsync(int id)=> db.GetEntryByIdAsync(id);

    
        public async Task<int> SaveEntryAsync(EntriesModel entry, int userId)
        {
            entry.UserId = userId;
            if (entry.Id == 0)
            {
                return await db.InsertEntryAsync(entry);
            }
            else
            {
                return await db.UpdateEntryAsync(entry);
            }
           
        }
        public async Task<AnalyticsModel> GetAnalyticsForUserAsync(int userId)
        {
            var analytics = await db.GetAnalyticsForUserAsync(userId);

            // Normalize MoodData into Positive / Neutral / Negative
            var groupedMoods = analytics.EntryData
            .GroupBy(e => MoodCategoryMap.GetCategory(e.Mood))
            .Select(g => new MoodInfo
            {
                Mood = g.Key,
                Value = g.Count()
            })
            .ToList();


            var allCategories = new[] { "Positive", "Neutral", "Negative" };
            foreach (var category in allCategories)
            {
                if (!groupedMoods.Any(m => m.Mood == category))
                    groupedMoods.Add(new MoodInfo { Mood = category, Value = 0 });
            }

            analytics.MoodData = groupedMoods;

            return analytics;
        }

        public Task<int> DeleteEntryAsync(int id) => db.DeleteEntryAsync(id);
    }
}
