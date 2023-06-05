using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CustomerService.Context;
using CustomerService.Interfaces;
using CustomerService.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICustomerType, CustomerTypeRepository>();
builder.Services.AddScoped<ICustomer, CustomerRepository>();

builder.Services.AddDbContext<CustomerContext>(options => options.UseSqlServer("name=ConnectionStrings:CustomerConnectionString", sqlServerOptions => sqlServerOptions.CommandTimeout(90)));
builder.Services.AddCors(options => options.AddPolicy("CustomerPolicy",
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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Services");
        //c.RoutePrefix = string.Empty;
    });
}

app.UseCors("CustomerPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
