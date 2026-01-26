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
        public async Task<bool> UpdateUserPasswordAsync(ResetPasswordModel model, bool isForgotFlow = false)
        {
            User? user;

            if (isForgotFlow)
            {
                // Forgot password: find by username
                user = await _db.GetUserByUsernameAsync(model.Username);
            }
            else
            {
                // Logged-in reset: find by current user ID
                if (!CurrentUserId.HasValue) return false;
                user = await _db.GetUserByIdAsync(CurrentUserId.Value);
            }

            if (user == null) return false;

            // In forgot flow, skip current password check
            if (!isForgotFlow && user.Password != model.CurrentPassword)
                return false;

            if (model.NewPassword != model.ConfirmPassword)
                return false;

            await _db.UpdateUserPasswordAsync(user.Id, model.NewPassword);
            return true;
        }

        public async Task<bool> DeleteCurrentUserAsync()
        {
            if (!CurrentUserId.HasValue) return false;

            await _db.DeleteUserAsync(CurrentUserId.Value);
            Logout(); 
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