using Grpc.Net.Client;
using Ordering.Server.Protos.Clients;
using Store.DataContext.SqlServer;
using Store.EntityModels.SqlServer;

namespace Ordering.Server.Clients;
public static class PaymentClient
{
    public static async Task<ResponseMessage> RequestPayment(List<Order> orders, PaymentsProcessor.PaymentsProcessorClient Client)
    {
        var newMessage = new OrderInformation();
        var stream = Client.ProcessOrder();
        foreach(var order in orders)
        {
           await stream.RequestStream.WriteAsync(new OrderInformation
            {
                 UserId=order.UserId,
                 Products=new Products{ Qantity=order.Quantity, ProductId=order.ProductId }
            });
        }
        await stream.RequestStream.CompleteAsync();
        var result = stream.ResponseAsync.Result;

        return result;
    }
}
