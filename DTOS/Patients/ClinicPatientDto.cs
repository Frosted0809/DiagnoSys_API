namespace DiagnoSys_API.DTOs
{
    public class ClinicPatientDto
    {
        public string PatientId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Sex { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Contact { get; set; }
        public DateTime? RegisteredAt { get; set; }
    }
}