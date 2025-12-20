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
        private readonly SQLiteConnection _db;

        public JournalDatabase(string dbPath)
        {
            _db = new SQLiteConnection(dbPath);
            _db.CreateTable<User>();
        }

        public int RegisterUser(User user) => _db.Insert(user);

        public User? GetUser(string username) =>
            _db.Table<User>().FirstOrDefault(u => u.Username == username);
    }

}
