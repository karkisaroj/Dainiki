using System;
using Dainiki.Components.Models;
using Dainiki.Components.Database;

namespace Dainiki.Components.Services
{
    public class AuthService
    {
        private readonly JournalDatabase _db;
        public event Action OnChange;
        public bool IsLoggedIn { get; private set; } = false;
        public string? CurrentUser { get; private set; }

        public AuthService(JournalDatabase db)
        {
            _db = db;
        }

        public bool Register(RegisterModel model)
        {
            if (_db.GetUser(model.Username) != null)
                return false;

            var user = new User
            {
                Username = model.Username,
                Password = model.Password 
            };
            _db.RegisterUser(user);
            return true;
        }

        public bool Login(string username, string password)
        {
            var user = _db.GetUser(username);
            if (user == null || user.Password != password)
                return false;
           
            IsLoggedIn = true;
            NotifyStateChanged();
            CurrentUser = username;
            
            return true;
        }
        private void NotifyStateChanged() => OnChange?.Invoke();
        public void Logout()
        {
            IsLoggedIn = false;
            CurrentUser = null;
            NotifyStateChanged();
            
        }
    }
}