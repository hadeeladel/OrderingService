using Grpc.Core;
using Ordering.Server.Core;
using Payment.Service.Protos;
using Store.DataContext.SqlServer;
using Store.EntityModels.SqlServer;


namespace Payment.Service.Services;

public class PaymentService : PaymentsProcessor.PaymentsProcessorBase
{
    private readonly ILogger<PaymentService> _logger;
    private IUnitOfWork _unitOF;
    //private UnitOFWork _unitOF;
    private readonly StoreContext context;

    public PaymentService(ILogger<PaymentService> logger,StoreContext storeContext, IUnitOfWork unitOFWork)
    {
        this._logger = logger;
        this.context = storeContext?? throw new ArgumentNullException(nameof(StoreContext)); ;
        this._unitOF = unitOFWork;

    }

    public override async Task<ResponseMessage> ProcessOrder(IAsyncStreamReader<OrderInformation> requestStream, ServerCallContext context)
    {
        double totalcost=0;
        int userid = 0;
            await foreach (var req in requestStream.ReadAllAsync())
            {
                _logger.LogInformation("doing request");
                Order order = new Order()
                {
                    ProductId = req.Products.ProductId,
                    OrderDate = DateTime.Now,
                    Quantity = req.Products.Qantity,
                    UserId = req.UserId
                };
                userid = req.UserId;
                totalcost += _unitOF.Order.GetOrderCost(order);
                _logger.LogInformation("got total cost of request");
            }
        _logger.LogInformation("withdraw from user");
       bool result= _unitOF.Order.WithdrawFromUser(userid,totalcost);
       
            ResponseMessage response = new ResponseMessage
            {
                Status = result,
                Details = "Withdraw from user Successfully "
            };
        //add order to the dtatbase and update the inventory 
        _unitOF.Complete();
        _unitOF.Dispose();
        _logger.LogInformation("payment is done");
        return response;
    }

 

}