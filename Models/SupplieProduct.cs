using System;
using System.Collections.Generic;

namespace PharmacyAIS.Models;

public partial class SupplieProduct
{
    public int SupplieId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Supplie Supplie { get; set; } = null!;
}
