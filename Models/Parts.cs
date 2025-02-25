using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace My_Project_03.Models;

public partial class Parts
{
    public int Id { get; set; }

    public string? PartNumber { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public int? CategoryId { get; set; }

    public string? VehicleType { get; set; }

    public string? CompatibilityNotes { get; set; }
    public ICollection<Cart> Carts { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
