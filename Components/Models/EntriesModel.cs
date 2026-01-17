public class EntriesModel
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public DateTime Date { get; set; }
    public TimeOnly Time { get; set; }

    public string Title { get; set; } = "";
    public string Content { get; set; } = "";

    public string PrimaryMood { get; set; } = "";
    public string SecondaryMoods { get; set; } = "";
    public string PhaseOfLife { get; set; } = "";

    public string Tags { get; set; } = "";            
    
}
