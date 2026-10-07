using System.ComponentModel.DataAnnotations;

namespace DiagnoSys_API.DTOs
{
    public class CreateLabTestCatalogDto
    {
        [Required(ErrorMessage = "Test name is required.")]
        [StringLength(100, ErrorMessage = "Test name cannot exceed 100 characters.")]
        public string TestName { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Standard price is required.")]
        [Range(0.01, 100000.00, ErrorMessage = "Price must be greater than 0.")]
        public decimal StandardPrice { get; set; }
    }
}