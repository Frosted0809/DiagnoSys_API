using System.ComponentModel.DataAnnotations;

namespace DiagnoSys_API.DTOs
{
    public class UpdateLabTestDto
    {
        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("^(Pending|In Progress|Completed|Cancelled)$", ErrorMessage = "Status must be Pending, In Progress, Completed, or Cancelled.")]
        public string Status { get; set; } = string.Empty;

        // usually di nag aalow pagpalit ng PatientId or TestId after ginawa si order
    }
}