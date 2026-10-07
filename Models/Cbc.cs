using System;
using System.Collections.Generic;

namespace DiagnoSys_API.Models;

public partial class Cbc
{
    public int CbcId { get; set; }

    public int OrderId { get; set; }

    public decimal? Wbc { get; set; }

    public decimal? Rbc { get; set; }

    public decimal? Hemoglobin { get; set; }

    public decimal? Hematocrit { get; set; }

    public int? Platelets { get; set; }

    public int? Mcv { get; set; }

    public int? Mch { get; set; }

    public decimal? Neutrophils { get; set; }

    public decimal? Lymphocytes { get; set; }

    public decimal? Monocytes { get; set; }

    public decimal? Eosinophils { get; set; }

    public decimal? Basophils { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual LabTest Order { get; set; } = null!;
}
