using Ordering.Server.Core;
using Payment.Service.Services;
using Store.DataContext.SqlServer;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddStoreContext();
builder.Services.AddGrpc();
builder.Services.AddStoreContext();
builder.Services.AddTransient<IUnitOfWork,UnitOFWork>();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PaymentService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
