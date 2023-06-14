using System;
using System.Collections.Generic;

namespace PharmacyAIS.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public virtual ICollection<Employee> Employee { get; set; } = new List<Employee>();
}
