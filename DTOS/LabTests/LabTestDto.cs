namespace DiagnoSys_API.DTOs
{
    public class LabTestDto
    {
        public int OrderId { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public int TestId { get; set; }
        public DateOnly OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;

        // Nested DTOs bale automatic sya ba (AutoMapper will fill these automatically) / yung ClinicPatients at labtest
        public ClinicPatientDto? Patient { get; set; }
        public LabTestCatalogDto? Test { get; set; }
    }
}