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

// get all users
app.MapGet("/api/users", (BangazonDbContext db) =>
{
    return db.Users.ToList();
});

// get all products
app.MapGet("/api/products", (BangazonDbContext db) =>
{
    return db.Products.ToList();
});

// get all categories
app.MapGet("/api/categories", (BangazonDbContext db) =>
{
    return db.Categories.ToList();
});

// get specific seller products
app.MapGet("/api/{sellerId}/products", (BangazonDbContext db, int sellerId) =>
{
    return db.Products.Where(u => u.SellerId == sellerId);
});

// get product details
app.MapGet("/api/products/{productId}", (BangazonDbContext db, int productId) =>
{
    return db.Products.Single(p => p.Id == productId);
});

// get complete orders
app.MapGet("/api/orders/complete", (BangazonDbContext db) =>
{
    return db.Orders.Where(o => o.IsComplete).ToList();
});

app.MapGet("/api/orders/{id}/history", (BangazonDbContext db, int id) =>
{
    return db.Orders.Where(u => u.CustomerId == id).ToList();
});

app.Run();
