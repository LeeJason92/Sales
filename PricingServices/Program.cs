using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PricingServices.Context;
using PricingServices.Interfaces;
using PricingServices.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IPricing, PricingRepository>();

builder.Services.AddDbContext<PricingContext>(options => options.UseSqlServer("name=ConnectionStrings:PricingConnectionString", sqlServerOptions => sqlServerOptions.CommandTimeout(90)));
builder.Services.AddCors(options => options.AddPolicy("PricingPolicy",
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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pricing Services");
        //c.RoutePrefix = string.Empty;
    });
}

app.UseCors("PricingPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
