using System.ComponentModel.DataAnnotations;

namespace DiagnoSys_API.DTOs
{
    public class CreateLabTestDto
    {
        [Required(ErrorMessage = "Patient ID is required.")]
        public string PatientId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Test ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Test ID.")]
        public int TestId { get; set; }

        // If date is not provided, controller will default to today puwede lagyan si date
        public DateOnly? OrderDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("^(Pending|In Progress|Completed|Cancelled)$", ErrorMessage = "Status must be Pending, In Progress, Completed, or Cancelled.")]
        public string Status { get; set; } = "Pending";
    }
}