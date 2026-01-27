using Dainiki.Components.Database;

namespace Dainiki.Components.Application.Services
{
    public class ThemeService
    {
        private readonly AuthService _auth;
        private readonly JournalDatabase _db;

        public bool IsDarkMode { get; private set; }

        public event Action? OnChange;

        public ThemeService(AuthService auth, JournalDatabase db)
        {
            _auth = auth;
            _db = db;
        }

        public async Task InitializeAsync()
        {
            IsDarkMode = _auth.IsDarkMode;

            if (_auth.IsLoggedIn && _auth.CurrentUserId.HasValue)
            {
                var user = await _db.GetUserByUsernameAsync(_auth.CurrentUser!);
                if (user != null)
                {
                    IsDarkMode = user.IsDarkMode;
                    _auth.IsDarkMode = user.IsDarkMode;
                }
            }

            Notify();
        }

        public async Task SetDarkModeAsync(bool value)
        {
            IsDarkMode = value;
            _auth.IsDarkMode = value;

            if (_auth.IsLoggedIn && _auth.CurrentUserId.HasValue)
            {
                await _db.UpdateUserThemePreferenceAsync(
                    _auth.CurrentUserId.Value,
                    value
                );
            }

            await _auth.UpdateThemePreferenceAsync(value);

            Notify();
        }

        private void Notify()
        {
            OnChange?.Invoke();
        }
    }
}
