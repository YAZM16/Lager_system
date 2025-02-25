using System;
using System.Collections.Generic;

namespace My_Project_03.Models;

public partial class Inventory
{
    public int Id { get; set; }

    public int? PartId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Parts? Parts { get; set; }
}
