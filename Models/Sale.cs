using System;
using System.Collections.Generic;

namespace PharmacyAIS.Models;

public partial class Sale
{
    public int SaleId { get; set; }

    public DateTime SaleDate { get; set; }

    public int SaleNumber { get; set; }

    public decimal TotalCost { get; set; }

    public virtual ICollection<SaleProduct> SaleProducts { get; set; } = new List<SaleProduct>();
}
