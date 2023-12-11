using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.EntityModels.SqlServer;

public class User
{
    [Key]
    public int UserId { get; set; }

    public string Username { get; set; }

    public double UserWallet { get; set; }
    public ICollection<Order> Orders { get; set; }

}
