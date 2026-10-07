namespace DiagnoSys_API.DTOs
{
    public class UpdateFecalysiDto
    {
        public string? Appearance { get; set; }
        public string? Consistency { get; set; }
        public string? OccultBlood { get; set; }
        public string? ParasiteId { get; set; }
        public string? Wbc { get; set; }
        public string? Rbc { get; set; }
        public string? Bacteria { get; set; }
        public string? OtherFindings { get; set; }
    }
}