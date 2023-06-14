using System;
using System.Collections.Generic;

namespace PharmacyAIS.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public int ManufacturerId { get; set; }

    public decimal Dosage { get; set; }

    public int UnitId { get; set; }

    public decimal Price { get; set; }

    public string Image { get; set; } = null!;

    public int QuantityInStock { get; set; }

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<ProductOrder> ProductOrder { get; set; } = new List<ProductOrder>();

    public virtual ICollection<SupplieProduct> SupplieProduct { get; set; } = new List<SupplieProduct>();

    public virtual Unit Unit { get; set; } = null!;
}
