using Period_Based_Encounter.Loader;

namespace Period_Based_Encounter;

public class Encounter
{
    public static void Main(string[] args)
    {
        var solutionDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
        var filePath = Path.Combine(solutionDirectory, "Input", "EncounterNotificationSampleData.csv");
        var loader = new AdtEventLoader();
        var data =       loader.LoadFromCsv(filePath);
        var processor = new EncounterProcessor();
        processor.ProcessRecords(data);
        var (validRecords, invalidRecords) = processor.GetRecords();
        var outptutProcessor = new EncounterOutputProcessor(validRecords, invalidRecords);
        outptutProcessor.ExportRecordsToCsvWithSections();
      
  

    }
}