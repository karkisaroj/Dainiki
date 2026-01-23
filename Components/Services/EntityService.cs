using Dainiki.Components.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dainiki.Components.Services
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

        public Task<EntriesModel?> GetEntryByIdAsync(int id)=> _db.GetEntryByIdAsync(id);

    
        public async Task<int> SaveEntryAsync(EntriesModel entry, int userId)
        {
            entry.UserId = userId;
            return await _db.SaveOrUpdateEntryAsync(entry);
        }
        public Task<int> DeleteEntryAsync(int id) => _db.DeleteEntryAsync(id);
    }
}
