using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace My_Project_03.Models;
public class Cart
{
    public int CartId { get; set; }
    public int UserId { get; set; }
    public int PartId { get; set; }
    public int Quantity { get; set; }
    public DateTime AddedDate { get; set; }

    public Parts Parts { get; set; }
}