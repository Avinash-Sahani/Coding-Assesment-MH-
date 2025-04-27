using Period_Based_Encounter.Models;

namespace Period_Based_Encounter.Helpers;

public static class ValidationHelper
{
    public static bool IsAdmit(EventType eventType)
    {
        return eventType == EventType.Admit;
    }

    public static bool IsDischarge(EventType eventType)
    {
        return eventType == EventType.Discharge;
    }

    public static bool IsHospitalNameSame(string hospitalName, string previousHospitalName)
    {
        return string.Equals(hospitalName, previousHospitalName, StringComparison.OrdinalIgnoreCase);
    }
    
    public static bool IsStartDateBeforeOrEqualEndDate(DateOnly? startDate, DateOnly? endDate)
    {
        if (startDate == null || endDate == null)
            return false;
        return startDate <= endDate;
    }
    
    public static bool HasMandatoryFields(AdtEventRecord record)
    {
        return record.PatientId != 0
               && !string.IsNullOrWhiteSpace(record.HospitalName)
               && record.EventDate != default
               && Enum.IsDefined(typeof(EventType), record.EventType);
    }
}
