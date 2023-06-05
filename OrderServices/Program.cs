using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderServices.Context;
using OrderServices.Interfaces;
using OrderServices.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ISalesOrder, SalesOrderRepository>();
builder.Services.AddScoped<ICancelOrder, CancelOrderRepository>();
builder.Services.AddScoped<IDeliveryOrder, DeliveryOrderRepository>();
builder.Services.AddScoped<IOrderDetail, OrderDetailRepository>();

builder.Services.AddDbContext<OrderContext>(options => options.UseSqlServer("name=ConnectionStrings:OrderConnectionString", sqlServerOptions => sqlServerOptions.CommandTimeout(90)));
builder.Services.AddCors(options => options.AddPolicy("OrderPolicy",
    policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen((options) =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "",
        Version = "V1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Services");
        //c.RoutePrefix = string.Empty;
    });
}

app.UseCors("OrderPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
