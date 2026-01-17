using System.ComponentModel.DataAnnotations;

namespace Dainiki.Components.ViewModels
{
    public class JournalEntryFormModel
    {
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "Primary mood is required")]
        public string PrimaryMood { get; set; } = string.Empty;

        public HashSet<string> SecondaryMoods { get; set; } = new();

        public string PhaseOfLife { get; set; } = string.Empty;

        public string TagsInput { get; set; } = string.Empty; 
    }
}