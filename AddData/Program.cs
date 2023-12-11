using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Store.DataContext.SqlServer;
using Store.EntityModels.SqlServer;

addData.ad();
public static class addData
{
    private static readonly StoreContext db;

    public static void ad()
    {
        Product a = new();
        a.ProductName = "jeans";
        a.Price = 20;
        a.availableInStock = 30;
        Product b = new();
        b.ProductName = "dress";
        b.availableInStock = 30;
        b.Price = 32;
        Product c = new();
        c.ProductName = "dress";
        c.availableInStock = 30;
        c.Price = 32;
        Product d = new();
        d.ProductName = "shirt";
        d.availableInStock = 30;
        d.Price = 32;
        db = new();
        db.Products.AddRange(a, b, c, d);
        //var x = new User { Username = "X", UserWallet = 200 };
        //var s = new User { Username = "Y", UserWallet = 300 };
        //var v = new User { Username = "Z", UserWallet = 120 };
        //db.Users.AddRange(x, s, v);
        db.SaveChanges();
        Console.WriteLine("done");
    }

}
