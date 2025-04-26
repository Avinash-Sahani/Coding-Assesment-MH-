namespace Period_Based_Encounter.Constants;


public static class ErrorMessages
{
    public const string DischargeBeforeAdmit = "Discharge event received before any admit event for patient {0}.";
    public const string DischargeDifferentHospital = "Discharge from hospital '{0}' does not match admit hospital '{1}'.";
    public const string DischargeBeforeAdmitDate = "Discharge date {1} is before admit date {0}.";
    public const string AdmitToNewHospitalWithoutDischarge = "Patient admitted to '{1}' without being discharged from previous hospital '{0}'.";
}