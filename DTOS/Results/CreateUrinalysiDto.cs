using System.ComponentModel.DataAnnotations;

namespace DiagnoSys_API.DTOs
{
    public class CreateUrinalysiDto
    {
        [Required(ErrorMessage = "Order ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Order ID.")]
        public int OrderId { get; set; }

        // All medical fields are optional for initial creation
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