using System;
using System.Collections.Generic;

namespace PharmacyAIS.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string ContactInfo { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<Order> Order { get; set; } = new List<Order>();
}
