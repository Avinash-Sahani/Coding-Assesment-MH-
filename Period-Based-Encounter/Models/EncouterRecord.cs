namespace Period_Based_Encounter.Models;

public class EncounterRecord
{
    public int PatientID { get; set; }
    public Guid EncounterID { get; set; }
    public string HospitalName { get; set; }
    public string? LevelOfCare { get; set; }
    public string? ChiefComplaint { get; set; }
    public int LengthOfStay { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }



}

