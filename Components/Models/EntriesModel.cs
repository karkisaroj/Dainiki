using System.ComponentModel.DataAnnotations;

public class EntriesModel
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public DateTime Date { get; set; }= DateTime.Now;
    public TimeOnly? Time { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

    [Required,StringLength(50)]
    public string Title { get; set; } = "";

    [Required]
    public string Content { get; set; } = "";

    [Required,StringLength(100)]
    public string PrimaryMood { get; set; } = "";
    public string SecondaryMoods { get; set; } = "[]";

    [StringLength(150)]
    public string PhaseOfLife { get; set; } = "";

    public string Tags { get; set; } = "[]";            
    
}
