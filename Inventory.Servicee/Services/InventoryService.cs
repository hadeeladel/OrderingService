using Grpc.Core;
using Inventory.Servicee.Protos;
using Ordering.Server.Core;
using Store.DataContext.SqlServer;
using Store.EntityModels.SqlServer;
using System.Collections.Specialized;

namespace Inventory.Servicee.Services;

public class InventoryService : InventoryManager.InventoryManagerBase
{

    private readonly ILogger<InventoryService> _logger;
    private readonly StoreContext Context;
    private IUnitOfWork _unitOF;
    public InventoryService(ILogger<InventoryService> logger,StoreContext storeContext,IUnitOfWork unitOfWork)
    {
        this._logger = logger;
        this.Context=storeContext;
        this._unitOF=unitOfWork;
        
    }

    public override async Task<PaymentResponceMessage> GetItemsFromInventory(IAsyncStreamReader<OrderMessage> requestStream, ServerCallContext context)
    {
        var response = new PaymentResponceMessage();
        response.Status = false;
        response.Details = "not enough in Stock for some or all products";
        int Count = 0;
        int AcutalCount = 0;
        await foreach (var req in requestStream.ReadAllAsync())
        {
            AcutalCount++;
            //i have product id and quantity
            Order order = new Order()
            {
                 UserId=req.Userid,
                 OrderDate=DateTime.Now,
                 ProductId=req.ItemId,
                 Quantity=req.Quantity,
            };
           bool result= _unitOF.Order.GetProductFromStore(order);
            if (result)
            {
                _unitOF.Order.Add(order);
                Count++;
            }
        }
        if(AcutalCount== Count)
        {
            response.Status = true;
            response.Details = "requested Products available in Stock";
            _unitOF.Complete();
        }
        return response;
    }
}
