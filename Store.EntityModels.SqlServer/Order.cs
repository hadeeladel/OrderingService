using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.EntityModels.SqlServer;

public class Order
{
    //every order is for one products 
    //more than one product then more than one order
    //that means when aregoing to send a stream of orders from the client to the server
    [Key]
    public int OrderId { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }

    public int Quantity { get; set; }
}
