namespace Period_Based_Encounter.Models
{ public class AdtEventRecord
    {
        public int EventId { get; set; } 
        public int PatientId { get; set; }
        public string HospitalName { get; set; }
        public DateOnly EventDate { get; set; }
        public EventType EventType { get; set; } 
        public string? LevelOfCare { get; set; }
        public string? ChiefComplaint { get; set; }

        

        public static EncounterRecord ToEncounter(AdtEventRecord admitRecord, AdtEventRecord dischargeRecord)
        {
            return new EncounterRecord
            {
                PatientID = admitRecord.PatientId,
                EncounterID = Guid.NewGuid(),
                HospitalName = admitRecord.HospitalName,
                LevelOfCare = admitRecord.LevelOfCare,
                ChiefComplaint = admitRecord.ChiefComplaint,
                StartDate = admitRecord.EventDate,
                EndDate = dischargeRecord.EventDate,
                LengthOfStay = dischargeRecord.EventDate.DayNumber == admitRecord.EventDate.DayNumber ? 1 : dischargeRecord.EventDate.DayNumber - admitRecord.EventDate.DayNumber,
            };
        }
        
        public InvalidEncounterRecord ToInvalid(string description)
        {
            return new InvalidEncounterRecord
            {
                PatientID = this.PatientId,
                EncounterID = Guid.NewGuid(),
                HospitalName = this.HospitalName,
                LevelOfCare = this.LevelOfCare,
                ChiefComplaint = this.ChiefComplaint,
                StartDate = this.EventDate,
                EndDate = null,
                LengthOfStay = 0,
                Description = description
            };
        }
        public EncounterRecord ToEncounter()
        {
            return new EncounterRecord
            {
                PatientID = this.PatientId,
                EncounterID = Guid.NewGuid(),
                HospitalName = this.HospitalName,
                LevelOfCare = this.LevelOfCare,
                ChiefComplaint = this.ChiefComplaint,
                StartDate = this.EventDate,
                EndDate = null,
                LengthOfStay = 0
            };
        }
    }
}