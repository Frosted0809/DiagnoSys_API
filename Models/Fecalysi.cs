using System;
using System.Collections.Generic;

namespace DiagnoSys_API.Models;

public partial class Fecalysi
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

    public virtual LabTest Order { get; set; } = null!;
}
