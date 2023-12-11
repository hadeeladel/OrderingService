using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Store.EntityModels.SqlServer;

namespace Store.DataContext.SqlServer;

public class StoreContext:DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options):base(options)
    {
        
    }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=StoreDb;Integrated Security=true;");
        }
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(d => d.UserId);    

        });

        modelBuilder.Entity<User>().HasData(new User { UserId=1 ,Username = "X", UserWallet = 200 },
            new User {UserId=2 ,Username = "Y", UserWallet = 300 },
            new User {  UserId=3,Username = "Z", UserWallet = 120 }
            );
        modelBuilder.Entity<Product>().HasData(new Product { ProductId = 4, ProductName = "jeans", Price = 20, availableInStock = 30 },
            new Product { ProductId = 1, ProductName = "socks", availableInStock = 30, Price = 5 },
             new Product { ProductId = 2, ProductName = "dress", availableInStock = 30, Price = 32 },
              new Product { ProductId = 3, ProductName = "shirt", availableInStock = 30, Price = 32 }
        );


    }
}
