using Microsoft.AspNetCore.Mvc;
using Store.EntityModels.SqlServer;
namespace Ordering.Server.Core.IRepositories;

public interface IOrderRepository:IRepository<Order>
{
    double GetOrderCost(Order order);
    bool WithdrawFromUser(int userid,double totalCost);
    bool GetProductFromStore(Order order);
}
