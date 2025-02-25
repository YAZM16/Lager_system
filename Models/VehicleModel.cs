using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace My_Project_03.Models;

public partial class VehicleModel
{
    public int ModelId { get; set; }

    public string ModelName { get; set; } = null!;

    public int? ManufacturerId { get; set; }

    public string? YearRange { get; set; }

    public string? Description { get; set; }

    public virtual Manufacturer? Manufacturer { get; set; }
}
