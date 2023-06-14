using System;
using System.Collections.Generic;

namespace PharmacyAIS.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public int OrderNumber { get; set; }

    public decimal TotalCost { get; set; }

    public int StatusId { get; set; }

    public DateTime? SaleDate { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<ProductOrder> ProductOrder { get; set; } = new List<ProductOrder>();

    public virtual OrderStatus Status { get; set; } = null!;
}
