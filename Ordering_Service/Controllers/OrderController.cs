using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ordering.Server.Clients;
using Ordering.Server.Protos.Clients;
using Store.DataContext.SqlServer;
using Store.EntityModels.SqlServer;
namespace Ordering.Server.Controllers;


[Route("api/[controller]")]
[ApiController]
public class OrderController : Controller
{
    private readonly PaymentsProcessor.PaymentsProcessorClient paymentsClient;
    private readonly InventoryManager.InventoryManagerClient inventoryClient;

    public OrderController(PaymentsProcessor.PaymentsProcessorClient paymentsClient,
                           InventoryManager.InventoryManagerClient inventoryClient)
    {
        this.paymentsClient = paymentsClient;
        this.inventoryClient = inventoryClient;
    }

    // GET: OrderController

    [HttpPost("with-di")]
    public async Task<IActionResult> PlaceOrderWithDi(List<Order> orders)
    {
        ResponseMessage payResponse = await PaymentClient.RequestPayment(orders, paymentsClient);
        bool storeResponce = false;
        if (payResponse.Status)
        {
            storeResponce = await InventoryClient.RequestFromInventory(orders, inventoryClient);
        }
        if (payResponse.Status && storeResponce) 
            return StatusCode(StatusCodes.Status200OK,$"Order Sucsess ,details:{payResponse.Details}");
        
        return StatusCode(StatusCodes.Status304NotModified,$"order Failed ,details:{payResponse.Details}");

    }
}





