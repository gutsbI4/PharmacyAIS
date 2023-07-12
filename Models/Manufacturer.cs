using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAIS.Models;

public partial class Manufacturer
{
    public int ManufacturerId { get; set; }
    [Required(ErrorMessage = "Название производителя не может быть пустым")]
    [StringLength(50, ErrorMessage = "Непоходящий размер названия", MinimumLength = 3)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Product { get; set; } = new List<Product>();
}
