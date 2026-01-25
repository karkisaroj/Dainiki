using Dainiki.Components.Database;
using Dainiki.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dainiki.Components.Application.Services
{
    public class EntityService
    {
        private readonly JournalDatabase _db;

        public EntityService(JournalDatabase db)
        {
            _db = db;
        }

        public async Task<int> UpdateEntryAsync(EntriesModel entry)
        {
            entry.UpdatedAt = DateTime.Now;
            return await _db.UpdateEntryAsync(entry);
        }

        public Task<List<EntriesModel>> GetEntriesByUserAsync(int userId)=> _db.GetEntriesByUserAsync(userId);

        public async Task<EntriesModel?> GetEntryByDateAsync(int userId, DateTime date)
        {
            return await _db.GetEntryByDateAsync(userId, date);
        }
        public Task<EntriesModel?> GetEntryByIdAsync(int id)=> _db.GetEntryByIdAsync(id);

    
        public async Task<int> SaveEntryAsync(EntriesModel entry, int userId)
        {
            entry.UserId = userId;
            if (entry.Id == 0)
            {
                return await _db.InsertEntryAsync(entry);
            }
            else
            {
                return await _db.UpdateEntryAsync(entry);
            }
           
        }
        public async Task<AnalyticsModel> GetAnalyticsForUserAsync(int userId)
        {
            return await _db.GetAnalyticsForUserAsync(userId);
        }

        public Task<int> DeleteEntryAsync(int id) => _db.DeleteEntryAsync(id);
    }
}
