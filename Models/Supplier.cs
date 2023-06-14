using System;
using System.Collections.Generic;

namespace PharmacyAIS.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string ContactInfo { get; set; } = null!;

    public virtual ICollection<Supplie> Supplie { get; set; } = new List<Supplie>();
}
