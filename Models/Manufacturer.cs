using System;
using System.Collections.Generic;

namespace PharmacyAIS.Models;

public partial class Manufacturer
{
    public int ManufacturerId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Product { get; set; } = new List<Product>();
}
