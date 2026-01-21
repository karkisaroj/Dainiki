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
        public async Task<int> SaveEntryAsync(EntriesModel entry)
        {
            return await _db.InsertAsync(entry);
        }
        public async Task<List<EntriesModel>> GetEntriesByUserAsync(int UserId)
        {
            return await _db.Table<EntriesModel>().Where(e=>e.UserId==UserId).ToListAsync();
        }
    }
}
