using Period_Based_Encounter.Constants;
using Period_Based_Encounter.Helpers;
using Period_Based_Encounter.Models;

namespace Period_Based_Encounter;

public class EncounterProcessor
{
    private readonly Dictionary<int, AdtEventRecord> _eventRecords = new();
    private readonly List<EncounterRecord> _validRecords = new();
    private readonly List<InvalidEncounterRecord> _invalidRecords = new();

    public (List<EncounterRecord>, List<InvalidEncounterRecord>) GetRecords()
    {
        return (_validRecords, _invalidRecords);
    }
    public void ProcessRecords(List<AdtEventRecord> records)
    {
        foreach (var record in records)
            ProcessRecord(record);

        AddRemainingAdmitOnlyRecords();
    }

    private void ProcessRecord(AdtEventRecord record)
    {
        if (!_eventRecords.TryGetValue(record.PatientId, out var currentRecord))
        {
            HandleNewRecord(record);
        }
        else
        {
            HandleExistingRecord(record, currentRecord);
        }
    }

    private void HandleNewRecord(AdtEventRecord record)
    {
        if (ValidationHelper.IsAdmit(record.EventType))
        {
            AddEventRecord(record);
        }
        else if (ValidationHelper.IsDischarge(record.EventType))
        {
            _invalidRecords.Add(record.ToInvalid(string.Format(ErrorMessages.DischargeBeforeAdmit, record.PatientId)));
        }
    }

    private void HandleExistingRecord(AdtEventRecord newRecord, AdtEventRecord currentRecord)
    {
        if (ValidationHelper.IsDischarge(newRecord.EventType))
        {
            ProcessDischarge(newRecord, currentRecord);
        }
        else if (ValidationHelper.IsAdmit(newRecord.EventType))
        {
            ProcessAdmit(newRecord, currentRecord);
        }
    }

    private void ProcessDischarge(AdtEventRecord discharge, AdtEventRecord admit)
    {
        if (!ValidationHelper.IsHospitalNameSame(discharge.HospitalName, admit.HospitalName))
        {
            _invalidRecords.Add(discharge.ToInvalid(string.Format(ErrorMessages.DischargeDifferentHospital, discharge.HospitalName, admit.HospitalName)));
            return;
        }

        if (!ValidationHelper.IsStartDateBeforeOrEqualEndDate(admit.EventDate, discharge.EventDate))
        {
            _invalidRecords.Add(discharge.ToInvalid(string.Format(ErrorMessages.DischargeBeforeAdmitDate, admit.EventDate, discharge.EventDate)));
            return;
        }

        _validRecords.Add(AdtEventRecord.ToEncounter(admit, discharge));
        _eventRecords.Remove(discharge.PatientId);
    }

    private void ProcessAdmit(AdtEventRecord newAdmit, AdtEventRecord oldAdmit)
    {
        if (!ValidationHelper.IsHospitalNameSame(newAdmit.HospitalName, oldAdmit.HospitalName))
        {
            _invalidRecords.Add(newAdmit.ToInvalid(string.Format(ErrorMessages.AdmitToNewHospitalWithoutDischarge, oldAdmit.HospitalName, newAdmit.HospitalName)));
            return;
        }
        oldAdmit.LevelOfCare = newAdmit.LevelOfCare;
        oldAdmit.ChiefComplaint = newAdmit.ChiefComplaint;
        oldAdmit.EventId = newAdmit.EventId;
    }

    private void AddEventRecord(AdtEventRecord record)
    {
        _eventRecords.Add(record.PatientId, record);
    }

    private void AddRemainingAdmitOnlyRecords()
    {
        foreach (var record in _eventRecords.Values)
        {
            _validRecords.Add(record.ToEncounter());
        }
    }
}
