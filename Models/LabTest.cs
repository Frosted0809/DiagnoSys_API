using System;
using System.Collections.Generic;

namespace DiagnoSys_API.Models;

public partial class LabTest
{
    public int OrderId { get; set; }

    public string PatientId { get; set; } = null!;

    public int TestId { get; set; }

    public DateOnly OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Cbc? Cbc { get; set; }

    public virtual Fecalysi? Fecalysi { get; set; }

    public virtual ClinicPatient Patient { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual LabTestCatalog Test { get; set; } = null!;

    public virtual Urinalysi? Urinalysi { get; set; }
}
