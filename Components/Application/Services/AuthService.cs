using System;
using Dainiki.Components.Domain.Models;
using Dainiki.Components.Database;
using System.Threading.Tasks;
namespace Dainiki.Components.Application.Services
{
    public class AuthService(JournalDatabase db)
    {
        private readonly JournalDatabase _db = db;
        public event Action? OnChange;
        public bool IsLoggedIn { get; private set; } = false;
        public string?CurrentUser { get; private set; }
        public string? CurrentUserName { get; set; }
        public int? CurrentUserId { get; private set; }
        public bool IsDarkMode { get; set; }
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
                Password = model.Password,
                IsDarkMode = false

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
            CurrentUserName = user.FirstName;
            IsDarkMode = user.IsDarkMode;
            NotifyStateChanged();  

            return true;
        }

        public void NotifyStateChanged() => OnChange?.Invoke();

        public async Task UpdateThemePreferenceAsync(bool isDarkMode)
        {
            if (CurrentUserId.HasValue)
            {
                await _db.UpdateUserThemePreferenceAsync(CurrentUserId.Value, isDarkMode);
                IsDarkMode = isDarkMode;
                NotifyStateChanged();
            }
        }
        public async Task<bool> UpdateUserPasswordAsync(ResetPasswordModel model)
        {
            if (!CurrentUserId.HasValue) return false;

            var user = await _db.GetUserByIdAsync(CurrentUserId.Value);
            if (user == null) return false;

            // Validate current password
            if (user.Password != model.CurrentPassword) return false;

            // Validate new password confirmation
            if (model.NewPassword != model.ConfirmPassword) return false;

            // Update in DB
            await _db.UpdateUserPasswordAsync(CurrentUserId.Value, model.NewPassword);
            return true;
        }




        public void Logout()
        {
            IsLoggedIn = false;
            CurrentUser = null;
            CurrentUserId = null;
            CurrentUserName = null;
            NotifyStateChanged();
        }
    }
}