using System;
using Dainiki.Components.Models;
using Dainiki.Components.Database;
using System.Threading.Tasks;
namespace Dainiki.Components.Services
{
    public class AuthService(JournalDatabase db)
    {
        private readonly JournalDatabase _db = db;
        public event Action? OnChange;
        public bool IsLoggedIn { get; private set; } = false;
        public string? CurrentUser { get; private set; }
        public int? CurrentUserId { get; private set; }
        public async Task<bool> Register(RegisterModel model)
        {
            var existing=await _db.GetUserByUsernameAsync(model.Username);
            if (existing != null)
            {
                return false;
            }
            var user = new User
            {
                FirstName = model.FirstName,
                Username = model.Username,
                Password = model.Password
            };
            await _db.RegisterUser(user);
            return true;
        }

        public async Task<bool> Login(string username, string password)
        {
            var user = await _db.ValidateLoginAsync(username,password);
            if (user == null || user.Password != password)
                return false;

            IsLoggedIn = true;
            CurrentUser = username;
            CurrentUserId = user.Id;
            NotifyStateChanged();  

            return true;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void Logout()
        {
            IsLoggedIn = false;
            CurrentUser = null;
            CurrentUserId = null;
            NotifyStateChanged();
        }
    }
}