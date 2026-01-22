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

        public async Task<int> SaveEntryAsync(EntriesModel entry, int userId)
        {
            entry.UserId = userId;
            entry.CreatedAt = DateTime.Now;
            entry.UpdatedAt = DateTime.Now;

            return await _db.SaveEntryAsync(entry);
        }

        public async Task<List<EntriesModel>> GetEntriesByUserAsync(int userId)
        {
            return await _db.GetEntriesByUserAsync(userId);
        }
    }
}
