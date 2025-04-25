namespace Period_Based_Encounter.Models;

public class AdtEventRecord
{
    public int EventId { get; set; } 
    public int PatientId { get; set; }
    public string HospitalName { get; set; }
    public DateOnly? EventDate { get; set; }
    public EventType EventType { get; set; } 
    public string? LevelOfCare { get; set; }
    public string? ChiefComplaint { get; set; }
    
    
}