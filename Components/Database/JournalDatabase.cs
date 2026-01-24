using Dainiki.Components.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dainiki.Components.Database
{
    public class JournalDatabase
    {
        private readonly SQLiteAsyncConnection _db;

        public JournalDatabase(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<User>().GetAwaiter().GetResult();
            _db.CreateTableAsync<EntriesModel>().GetAwaiter().GetResult();
        }

        public async Task<int> RegisterUser(User user)
        {
            return await _db.InsertAsync(user);
        }

        public async Task<User?> ValidateLoginAsync(string username, string password)
        {
            return await _db.Table<User>().FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
        public async Task<int> InsertEntryAsync(EntriesModel entry)
        {
            entry.CreatedAt = DateTime.Now;
            entry.UpdatedAt = DateTime.Now;
            return await _db.InsertAsync(entry); 
        }

        public async Task<EntriesModel?> GetEntryByDateAsync(int userId, DateTime date)
        {
            return await _db.Table<EntriesModel>()
                .FirstOrDefaultAsync(e => e.UserId == userId && e.Date == date);
        }

        public async Task<int> UpdateEntryAsync(EntriesModel entry)
        {
            return await _db.UpdateAsync(entry); 
        }
        public async Task<User?> GetUserByUsernameAsync(string username) =>
           await _db.Table<User>().FirstOrDefaultAsync(u => u.Username == username);
        public async Task UpdateUserThemePreferenceAsync(int userId, bool isDarkMode)
        {
            var user = await _db.Table<User>().FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                user.IsDarkMode = isDarkMode;
                await _db.UpdateAsync(user);
            }
        }
        public async Task<List<EntriesModel>> GetEntriesByUserAsync(int UserId)
        {
            return await _db.Table<EntriesModel>().Where(e => e.UserId == UserId).ToListAsync();
        }
        public async Task<EntriesModel?> GetEntryByIdAsync(int id)
             => await _db.Table<EntriesModel>().FirstOrDefaultAsync(e => e.Id == id);

        
        public async Task<int> DeleteEntryAsync(int id)
        {
            var entry = await GetEntryByIdAsync(id);
            return entry != null ? await _db.DeleteAsync(entry) : 0;
        }
    }
}
