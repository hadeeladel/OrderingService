//using Microsoft.AspNetCore.Server.Kestrel.Core;
using Store.DataContext.SqlServer;
using Ordering.Server.Protos.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddStoreContext();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//You can do it like this instead of the wrapper classes you make

builder.Services.AddGrpcClient<PaymentsProcessor.PaymentsProcessorClient>(options =>
{
    options.Address = new Uri("https://localhost:5050");
});
builder.Services.AddGrpcClient<InventoryManager.InventoryManagerClient>(options =>
{
    options.Address = new Uri("https://localhost:7215");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
