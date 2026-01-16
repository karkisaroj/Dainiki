namespace Dainiki.Components.Models
{
    public class EntriesModel
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public TimeOnly? Time { get; set; }
        public string PrimaryMood { get; set; } = string.Empty;
        public List<string> SecondaryMoods { get; set; } = new List<string>();
        public string PhaseOfLife { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Preview { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new List<string>();
    }
}