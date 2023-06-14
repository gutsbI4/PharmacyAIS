using System;
using System.Collections.Generic;

namespace PharmacyAIS.Models;

public partial class OrderStatus
{
    public int OrderStatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Order { get; set; } = new List<Order>();
}
