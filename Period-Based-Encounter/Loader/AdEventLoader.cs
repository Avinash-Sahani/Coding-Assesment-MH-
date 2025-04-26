using Period_Based_Encounter.Models;

namespace Period_Based_Encounter.Loader;


using Deedle;

public class AdtEventLoader
{
   
    public List<AdtEventRecord> LoadFromCsv(string filePath)
    {
         var records = new List<AdtEventRecord>();
      Console.WriteLine($"Reading and Processing");
        var frame = Frame.ReadCsv(filePath);
        var rows = frame.Rows;
        for(int i=0; i<frame.RowCount; i++)
        {
            var record = CreateRecordFromRow(rows[i]);
            records.Add(record);
        }

        return records;
    }

    private AdtEventRecord CreateRecordFromRow(ObjectSeries<string> row)
    {
        return new AdtEventRecord
        {
            EventId =   row.GetAs<int>("EventID"),
            PatientId = row.GetAs<int>("PatientID"),
            HospitalName = row.GetAs<string>("HospitalName"),
            EventDate = DateTime.TryParse(row.GetAs<string>("EventDate"), out var dt)
                ? DateOnly.FromDateTime(dt)
                : DateOnly.MinValue,
            EventType =  Enum.TryParse<EventType>(row.GetAs<string>("EventType"), ignoreCase: true, out var parsedEventType)
                ? parsedEventType
                : EventType.None,
            LevelOfCare = string.IsNullOrEmpty(row.GetAs<string>("LevelOfCare")) ? null : row.GetAs<string>("LevelOfCare"),
            ChiefComplaint = string.IsNullOrEmpty(row.GetAs<string>("Chief Complaint")) ? null : row.GetAs<string>("Chief Complaint")
        };
    }
    
}
