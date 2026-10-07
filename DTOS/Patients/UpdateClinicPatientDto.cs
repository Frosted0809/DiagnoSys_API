using System.ComponentModel.DataAnnotations;

namespace DiagnoSys_API.DTOs
{
    public class UpdateClinicPatientDto
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Range(0, 150)]
        public int Age { get; set; }

        [Required(ErrorMessage = "Sex is required")]
        [RegularExpression("M|F", ErrorMessage = "Sex must be M or F")]
        public string Sex { get; set; } = string.Empty;

        [StringLength(150)]
        public string? Address { get; set; }

        [StringLength(20)]
        public string? Contact { get; set; }
    }
}