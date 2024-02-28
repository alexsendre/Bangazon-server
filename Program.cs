using BangazonBE.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using BangazonBE;
using System.Data.Common;
using BangazonBE.DTOs;

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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5003")
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// USER DATA \\

// get all users
app.MapGet("/api/users", (BangazonDbContext db) =>
{
    return db.Users.ToList();
});

// get (self) profile data
app.MapGet("/api/users/{id}", (BangazonDbContext db, int id) =>
{
    return db.Users.Where(u => u.Id == id).ToList();
});

// PRODUCT DATA \\

// get all products
app.MapGet("/api/products", (BangazonDbContext db) =>
{
    return db.Products.ToList();
});

// get specific seller products
app.MapGet("/api/{sellerId}/products", (BangazonDbContext db, int sellerId) =>
{
    return db.Products.Where(u => u.SellerId == sellerId).ToList();
});

// get product details
app.MapGet("/api/products/{productId}", (BangazonDbContext db, int productId) =>
{
    return db.Products.Single(p => p.Id == productId);
});

// ORDER DATA \\

// get order history
app.MapGet("/api/orders/{id}/history", (BangazonDbContext db, int id) =>
{
    return db.Orders.Where(u => u.CustomerId == id).ToList();
});

// create order
app.MapPost("/api/orders", (BangazonDbContext db, Order newOrder) =>
{
    try
    {
        db.Orders.Add(newOrder);
        db.SaveChanges();
        return Results.Created($"/api/orders/{newOrder.Id}", newOrder);
    }
    catch (DbException)
    {
        return Results.BadRequest("Something went wrong, invalid submission.");
    }
});

// add to cart
app.MapPost("/api/orders/add", (BangazonDbContext db, AddToCartDTO newItem) =>
{
    var order = db.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == newItem.OrderId);
    var productToAdd = db.Products.Find(newItem.ProductId);

    if (order == null || productToAdd == null)
    {
        return Results.NotFound();
    }

    try
    {
        order.Products.Add(productToAdd);
        db.SaveChanges();
        return Results.Created($"/api/orders/{newItem.OrderId}/products/{newItem.ProductId}", productToAdd);
    }
    catch
    {
        return Results.BadRequest("There was an error with the data submitted");
    }
});

// delete from cart
app.MapDelete("/api/orders/{orderId}/products/{productId}", (BangazonDbContext db, int orderId, int productId) =>
{
    var order = db.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == orderId);
    var product = db.Products.Find(productId);

    if (order == null || product == null)
    {
        return Results.NotFound("Invalid data request");
    }

    order.Products.Remove(product);
    db.SaveChanges();
    return Results.NoContent();
});

// update order
app.MapPut("/api/orders/{id}/edit", (BangazonDbContext db, int orderId, Order updateInfo) =>
{
    Order orderToUpdate = db.Orders.SingleOrDefault(o => o.Id == orderId);

    if (orderToUpdate == null)
    {
        return Results.NotFound("Couldn't find the requested order");
    }

    orderToUpdate.PaymentTypeId = updateInfo.PaymentTypeId;
    orderToUpdate.IsComplete = updateInfo.IsComplete;

    db.SaveChanges();
    return Results.NoContent();
});

// get complete orders
app.MapGet("/api/orders/complete", (BangazonDbContext db) =>
{
    return db.Orders.Where(o => o.IsComplete).ToList();
});

// CATEGORY DATA \\

// get all categories
app.MapGet("/api/categories", (BangazonDbContext db) =>
{
    return db.Categories.ToList();
});

// get products by category
app.MapGet("/api/categories/{categoryId}", (BangazonDbContext db, int categoryId) =>
{
    return db.Products.Where(u => u.CategoryId == categoryId);
});

// create category
app.MapPost("/api/categories", (BangazonDbContext db, Category category) =>
{
    db.Categories.Add(category);
    db.SaveChanges();
    return Results.Created($"/api/categories/{category.Id}", category);
});

app.MapDelete("/api/categories/{id}", (BangazonDbContext db, int id) =>
{
    var category = db.Categories.SingleOrDefault(category => category.Id == id);

    if (category == null)
    {
        return Results.NotFound();
    }

    db.Categories.Remove(category);
    db.SaveChanges();
    return Results.NoContent();
});

// PAYMENT DATA \\

// get all payment types
app.MapGet("/api/payments", (BangazonDbContext db) =>
{
    return db.PaymentTypes.ToList();
});

// SEARCH \\

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

// AUTH DATA \\

app.MapPost("/api/register", (BangazonDbContext db, User user) =>
{
    db.Users.Add(user);
    db.SaveChanges();
    return Results.Created($"/api/users/{user.Id}", user);
});

app.MapGet("/api/checkuser/{uid}", (BangazonDbContext db, string uid) =>
{
    var existingUser = db.Users.Where(u => u.Uid == uid).ToList();
    if (existingUser == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(existingUser);
});


app.Run();
