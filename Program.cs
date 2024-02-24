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

// get order history
app.MapGet("/api/orders/{id}/history", (BangazonDbContext db, int id) =>
{
    return db.Orders.Where(u => u.CustomerId == id).ToList();
});

// get (self) profile data
app.MapGet("/api/users/{id}", (BangazonDbContext db, int id) =>
{
    return db.Users.Where(u => u.Id ==  id).ToList();
});

// get products by category
app.MapGet("/api/categories/{categoryId}", (BangazonDbContext db, int categoryId) =>
{
    return db.Products.Where(u => u.CategoryId == categoryId);
});

// search for products or sellers (case sensitive)
app.MapGet("/api/search", (BangazonDbContext db, string query) =>
{
    if (string.IsNullOrWhiteSpace(query))
    {
        return Results.BadRequest("Search query cannot be empty.");
    }

    var products = db.Products.Where(p => p.Title.Contains(query)).ToList();
    var users = db.Users.Where(p => p.FirstName.Contains(query) || p.LastName.Contains(query)).ToList();

    var responseData = new
    {
        products,
        users
    };

    if (products.Count == 0 && users.Count == 0)
    {
        return Results.NotFound("No results found.");
    }
    else
    {
        return Results.Ok(responseData);
    }
});

app.MapPost("/api/orders", (BangazonDbContext db, Order newOrder) =>
{
    db.Orders.Add(newOrder);
    db.SaveChanges();
    return Results.Created($"/api/orders/{newOrder.Id}", newOrder);
});

app.Run();
