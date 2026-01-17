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
           
        }

        public async Task<int> RegisterUser(User user)
        {
            
           return await _db.InsertAsync(user);
        }

        public async Task<User?> ValidateLoginAsync(string username,string password)
        {
            return await _db.Table<User>().FirstOrDefaultAsync(u=>u.Username == username && u.Password == password);
        }


        public async Task< User?> GetUserByUsernameAsync(string username) =>
           await _db.Table<User>().FirstOrDefaultAsync(u => u.Username == username );
    }

}
