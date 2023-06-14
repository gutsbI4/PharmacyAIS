using System;
using System.Collections.Generic;

namespace PharmacyAIS.Models;

public partial class Supplie
{
    public int SupplieId { get; set; }

    public int SupplierId { get; set; }

    public int Quantity { get; set; }

    public decimal TotalCost { get; set; }

    public virtual ICollection<SupplieProduct> SupplieProduct { get; set; } = new List<SupplieProduct>();

    public virtual Supplier Supplier { get; set; } = null!;
}
