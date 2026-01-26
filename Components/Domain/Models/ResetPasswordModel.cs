namespace Dainiki.Components.Domain.Models
{
    public class ResetPasswordModel
    {
        public string Username { get; set; } = string.Empty; 
        public string CurrentPassword { get; set; } = string.Empty; 
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}