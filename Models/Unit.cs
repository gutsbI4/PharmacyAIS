using System;
using System.Collections.Generic;

namespace PharmacyAIS.Models;

public partial class Unit
{
    public int UnitId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Product { get; set; } = new List<Product>();
}
