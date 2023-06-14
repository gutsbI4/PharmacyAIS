using System;
using System.Collections.Generic;

namespace PharmacyAIS.Models;

public partial class SaleProduct
{
    public int SaleId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Sale Sale { get; set; } = null!;
}
