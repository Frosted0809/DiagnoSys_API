using System.ComponentModel.DataAnnotations;

namespace DiagnoSys_API.DTOs
{
    public class CreateFecalysiDto
    {
        [Required(ErrorMessage = "Order ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Order ID.")]
        public int OrderId { get; set; }

        // All medical fields are optional for initial creation
        // same lng din sa mga ginawa ng cbc at urinalisys
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