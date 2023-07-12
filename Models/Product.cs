using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace PharmacyAIS.Models;

public partial class Product
{
    public int ProductId { get; set; }
    [Required(ErrorMessage = "Не может быть пустым")]
    [StringLength(50,ErrorMessage ="Непоходящий размер названия",MinimumLength =3)]
    public string Name { get; set; } = null!;
    public int ManufacturerId { get; set; }
    [Range(1, 500000.00, ErrorMessage = "Неприемлемая доза")]
    public decimal Dosage { get; set; }
    public int UnitId { get; set; }
    [Range(1, 500000.00, ErrorMessage = "Неприемлемая цена")]
    public decimal Price { get; set; }

    public string Image { get; set; } = null!;
    [Range(0, 10000, ErrorMessage = "Количество должно быть от {1} до {2}")]
    public int QuantityInStock { get; set; }

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<ProductOrder> ProductOrder { get; set; } = new List<ProductOrder>();

    public virtual ICollection<SupplieProduct> SupplieProduct { get; set; } = new List<SupplieProduct>();

    public virtual Unit Unit { get; set; } = null!;
}
