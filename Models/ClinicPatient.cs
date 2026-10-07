using System;
using System.Collections.Generic;

namespace DiagnoSys_API.Models;

public partial class ClinicPatient
{
    public string PatientId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Age { get; set; }

    public string Sex { get; set; } = null!;

    public string? Address { get; set; }

    public string? Contact { get; set; }

    public DateTime? RegisteredAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<LabTest> LabTests { get; set; } = new List<LabTest>();
}
