using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StoreServices.Context;
using StoreServices.Interfaces;
using StoreServices.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IStore, StoreRepository>();
builder.Services.AddScoped<IMarketingArea, MarketingAreaRepository>();

builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer("name=ConnectionStrings:StoreConnectionString", sqlServerOptions => sqlServerOptions.CommandTimeout(90)));
builder.Services.AddCors(options => options.AddPolicy("StorePolicy",
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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store Services");
        //c.RoutePrefix = string.Empty;
    });
}

app.UseCors("StorePolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
