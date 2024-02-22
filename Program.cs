using BangazonBE.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using BangazonBE;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddNpgsql<BangazonDbContext>(builder.Configuration["BangazonBEDbConnectionString"]);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/users", (BangazonDbContext db) =>
{
    return db.Users.ToList();
});

app.MapGet("/api/products", (BangazonDbContext db) =>
{
    return db.Products.ToList();
});

app.MapGet("/api/categories", (BangazonDbContext db) =>
{
    return db.Categories.ToList();
});

app.MapGet("/api/{sellerId}/products", (BangazonDbContext db, int sellerId) =>
{
    return db.Products.Single(u => u.SellerId == sellerId);
});

app.MapGet("/api/products/{productId}", (BangazonDbContext db, int productId) =>
{
    return db.Products.Single(p => p.Id == productId);
});

app.MapGet("/api/orders/complete", (BangazonDbContext db) =>
{
    return db.Orders.Where(o => o.IsComplete).ToList();
});

app.Run();
