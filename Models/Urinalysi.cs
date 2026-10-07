using System;
using System.Collections.Generic;

namespace DiagnoSys_API.Models;

public partial class Urinalysi
{
    public int UaId { get; set; }

    public int OrderId { get; set; }

    public string? Appearance { get; set; }

    public string? Color { get; set; }

    public decimal? Ph { get; set; }

    public decimal? SpecificGravity { get; set; }

    public string? Glucose { get; set; }

    public string? Protein { get; set; }

    public string? Ketones { get; set; }

    public string? Nitrites { get; set; }

    public string? OtherFindings { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual LabTest Order { get; set; } = null!;
}
