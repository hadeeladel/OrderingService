using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.EntityModels.SqlServer;

public class Product
{

    [Key]
    public int ProductId { get; set; }

    public string ProductName { get; set; }

    [Required]
    public int availableInStock { get; set; }

    public double Price { get; set; }
}
