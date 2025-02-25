using System;
using System.Collections.Generic;

namespace My_Project_03.Models;

public partial class VehicleType
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }
}
