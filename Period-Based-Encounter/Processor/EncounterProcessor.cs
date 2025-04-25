using Period_Based_Encounter.Helpers;
using Period_Based_Encounter.Models;

namespace Period_Based_Encounter;

public class EncounterProcessor
{

    public Dictionary<int, AdtEventRecord> _eventRecords;
   
    public EncounterProcessor()
    {

        _eventRecords = new Dictionary<int, AdtEventRecord>();
    }
    
    public void ProcessRecord(AdtEventRecord record)
    {

        if (!ContainsRecord(record.PatientId))
        {
            if(ValidationHelper.IsAdmit(record.EventType))
                    AddEventRecord(record);
            
            if (ValidationHelper.IsDischarge(record.EventType))
            {
                // Its a Invalid Record
            }
            
        }
        else
        {
            var currentRecord = _eventRecords[record.PatientId];
            if (ValidationHelper.IsDischarge(record.EventType))
            {
                if (ValidationHelper.IsHospitalNameSame(record.HospitalName, currentRecord.HospitalName))
                {
                    if (ValidationHelper.IsStartDateBeforeOrEqualEndDate(currentRecord.EventDate, record.EventDate))
                    {
                        // Process the record
                    }
                }
            }
        }
    }

    private bool ContainsRecord(int PatinetId) => _eventRecords.ContainsKey(PatinetId);
    private void AddEventRecord(AdtEventRecord record) => _eventRecords.Add(record.PatientId, record);
}