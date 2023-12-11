using Ordering.Server.Core.IRepositories;
using Ordering.Server.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.EntityModels.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace Store.DataContext.SqlServer.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(StoreContext context)
            :base(context)
        {
            
        }
        public StoreContext StoreContext
        {
            get { return Context as StoreContext; }
        }
        public double GetOrderCost(Order order)
        {
           var product= StoreContext.Products.FirstOrDefault(p => p.ProductId.Equals(order.ProductId));
            if(product is not null)
            {
                var result = product.Price * order.Quantity;
                return result;
            }
            throw new ArgumentNullException(nameof(order));
            
        }

        public bool GetProductFromStore(Order order)
        {
            var product= StoreContext.Products?.FirstOrDefault(p=>p.ProductId.Equals(order.ProductId));
            if (product is not null)
            {
                int result = product.availableInStock - order.Quantity;
                if(result < 0) { return false; }
                product.availableInStock = result;
                return true;
            }
            return false;

        }

        public bool WithdrawFromUser(int userid,double totalCost)
        {
           var userData= StoreContext.Users?.First(u => u.UserId.Equals(userid));
            if (userData == null) { return false ; }
            double result= userData.UserWallet - totalCost;
            if(result <0) { return false; }
            userData.UserWallet = result;
            return true;
        }
    }
}
