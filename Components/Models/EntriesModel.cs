using SQLite;
using System.ComponentModel.DataAnnotations;
namespace Dainiki.Components.Models
{
    public class EntriesModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public TimeSpan? Time { get; set; } = DateTime.Now.TimeOfDay;

        [Required, StringLength(50)]
        public string Title { get; set; } = "";

        [Required]
        public string Content { get; set; } = "";

        [Required(ErrorMessage = "Primary mood is required"), StringLength(100)]
        public string PrimaryMood { get; set; } = "";
        public string SecondaryMoods { get; set; } = "[]";

        [StringLength(150)]
        public string PhaseOfLife { get; set; } = "[]";

        public string Tags { get; set; } = "[]";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

