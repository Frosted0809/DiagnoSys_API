using System;
using System.Collections.Generic;

namespace DiagnoSys_API.Models;

public partial class LabTestCatalog
{
    public int TestId { get; set; }

    public string TestName { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<LabTest> LabTests { get; set; } = new List<LabTest>();
}
