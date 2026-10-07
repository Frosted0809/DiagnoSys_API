namespace DiagnoSys_API.DTOs
{
    public class FecalysiDto
    {
        public int FaId { get; set; }
        public int OrderId { get; set; }

        public string? Appearance { get; set; }
        public string? Consistency { get; set; }
        public string? OccultBlood { get; set; }
        public string? ParasiteId { get; set; }
        public string? Wbc { get; set; }
        public string? Rbc { get; set; }
        public string? Bacteria { get; set; }
        public string? OtherFindings { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Nested order info
        public LabTestDto? Order { get; set; }
    }
}