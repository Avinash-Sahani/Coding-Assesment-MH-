using ClosedXML.Excel;
using Period_Based_Encounter.Models;

namespace Period_Based_Encounter
{
    public class EncounterOutputProcessor
    {
        private readonly List<EncounterRecord> _validRecords;
        private readonly List<InvalidEncounterRecord> _invalidRecords;

        public EncounterOutputProcessor(List<EncounterRecord> validRecords, List<InvalidEncounterRecord> invalidRecords)
        {
            _validRecords = validRecords;
            _invalidRecords = invalidRecords;
        }

        public void ExportRecordsToExcel()
        {
            var solutionDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
            var filePath = Path.Combine(solutionDirectory, "Output", "Encounter_Records_Output.xlsx");
            DeleteFileIfExists(filePath);
            using var workbook = new XLWorkbook();
            var validSheet = workbook.Worksheets.Add("Valid Records");
            validSheet.Cell(1, 1).InsertTable(_validRecords);

            var invalidSheet = workbook.Worksheets.Add("Invalid Records");
            var reorderedInvalids = _invalidRecords.Select(r => new
            {
                r.PatientID,
                r.EncounterID,
                r.HospitalName,
                r.LevelOfCare,
                r.ChiefComplaint,
                r.LengthOfStay,
                r.StartDate,
                r.EndDate,
                r.Description
            });
            invalidSheet.Cell(1, 1).InsertTable(reorderedInvalids);

            workbook.SaveAs(filePath);
        }
        
        private void DeleteFileIfExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}