using System;
using System.Collections.Generic;

namespace My_Project_03.Models;

public partial class Manufacturer
{
    public int ManufacturerId { get; set; }

    public string Name { get; set; } = null!;

    public string? CountryOfOrigin { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<VehicleModel> VehicleModels { get; set; } = new List<VehicleModel>();
}
