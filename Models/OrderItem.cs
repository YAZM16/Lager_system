using System;
using System.Collections.Generic;

namespace My_Project_03.Models;

public partial class OrderItem
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? InventoryId { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public virtual Inventory? Inventory { get; set; }
}
