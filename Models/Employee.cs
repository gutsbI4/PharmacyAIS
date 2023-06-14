using System;
using System.Collections.Generic;

namespace PharmacyAIS.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string Position { get; set; } = null!;

    public decimal Salary { get; set; }

    public string ContactInfo { get; set; } = null!;

    public int DepartmentId { get; set; }

    public string LastName { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<User> User { get; set; } = new List<User>();
}
