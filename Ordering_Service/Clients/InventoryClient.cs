using Grpc.Net.Client;
using Ordering.Server.Protos.Clients;
using Store.DataContext.SqlServer;
using Store.EntityModels.SqlServer;
namespace Ordering.Server.Clients;

public static class InventoryClient
{

    public static async Task<bool> RequestFromInventory(List<Order> orders, InventoryManager.InventoryManagerClient Client)
    {
        using var stream = Client.GetItemsFromInventory();

        foreach (var order in orders)
        {
            await stream.RequestStream.WriteAsync(new OrderMessage
            {
                Userid=order.UserId,
                ItemId = order.ProductId,
                Quantity = order.Quantity,
            });
        }
        await stream.RequestStream.CompleteAsync();

        return stream.ResponseAsync.Result.Status;
    }


}