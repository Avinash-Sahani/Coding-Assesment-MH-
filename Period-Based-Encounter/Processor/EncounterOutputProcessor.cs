using Deedle;
using Period_Based_Encounter.Models;

namespace Period_Based_Encounter;

public class EncounterOutputProcessor
{

    private List<EncounterRecord> _validRecords;
    private List<InvalidEncounterRecord> _invalidRecords;

    public EncounterOutputProcessor(List<EncounterRecord> validRecords, List<InvalidEncounterRecord> invalidRecords)
    {
        
            _validRecords = validRecords;
            _invalidRecords = invalidRecords;
    }

    
    public void ExportRecordsToCsvWithSections()
    {
        var solutionDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
        var filePath = Path.Combine(solutionDirectory, "Output", "output.csv");

        using var writer = new StreamWriter(filePath);

        writer.WriteLine("Valid Records");
        writer.WriteLine("PatientID,EncounterID,HospitalName,LevelOfCare,ChiefComplaint,LengthOfStay,StartDate,EndDate");

        foreach (var r in _validRecords)
        {
            writer.WriteLine($"{r.PatientID},{r.EncounterID},{r.HospitalName},{r.LevelOfCare},{r.ChiefComplaint},{r.LengthOfStay},{r.StartDate},{r.EndDate}");
        }

        writer.WriteLine(); 
        writer.WriteLine("Invalid Records");
        writer.WriteLine("PatientID,EncounterID,HospitalName,LevelOfCare,ChiefComplaint,LengthOfStay,StartDate,EndDate,Description");

        foreach (var r in _invalidRecords)
        {
            writer.WriteLine($"{r.PatientID},{r.EncounterID},{r.HospitalName},{r.LevelOfCare},{r.ChiefComplaint},{r.LengthOfStay},{r.StartDate},{r.EndDate},{r.Description}");
        }
    }

}