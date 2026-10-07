namespace DiagnoSys_API.DTOs
{
    public class UpdateUrinalysiDto
    {
        public string? Appearance { get; set; }
        public string? Color { get; set; }
        public decimal? Ph { get; set; }
        public decimal? SpecificGravity { get; set; }
        public string? Glucose { get; set; }
        public string? Protein { get; set; }
        public string? Ketones { get; set; }
        public string? Nitrites { get; set; }
        public string? OtherFindings { get; set; }
    }
}