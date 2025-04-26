using Period_Based_Encounter.Loader;
using System;
using Period_Based_Encounter.Constants;
using Period_Based_Encounter.Models;

namespace Period_Based_Encounter
{
    public class Encounter
    {
 

        public static void Main(string[] args)
        {
            var filePath = BuildInputFilePath();

            var eventRecords = LoadEventRecords(filePath);

            var (validRecords, invalidRecords) = ProcessEventRecords(eventRecords);

            ExportResults(validRecords, invalidRecords);
        }

        private static string BuildInputFilePath()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var solutionDir = Path.GetFullPath(Path.Combine(baseDir, "..", "..", ".."));
            return Path.Combine(solutionDir, DirectoryConstants.InputFolderName, DirectoryConstants.InputFileName);
        }

        private static List<AdtEventRecord> LoadEventRecords(string filePath)
        {
            var loader = new AdtEventLoader();
            return loader.LoadFromCsv(filePath);
        }

        private static (List<EncounterRecord>, List<InvalidEncounterRecord>) ProcessEventRecords(List<AdtEventRecord> records)
        {
            var processor = new EncounterProcessor();
            processor.ProcessRecords(records);
            return processor.GetRecords();
        }

        private static void ExportResults(List<EncounterRecord> validRecords, List<InvalidEncounterRecord> invalidRecords)
        {
            var outputProcessor = new EncounterOutputProcessor(validRecords, invalidRecords);
            outputProcessor.ExportRecordsToCsvWithSections();
        }
    }
}