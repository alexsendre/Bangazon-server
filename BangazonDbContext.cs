using BangazonBE.Models;
using Microsoft.EntityFrameworkCore;

namespace BangazonBE
{
    public class BangazonDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }

        public BangazonDbContext(DbContextOptions<BangazonDbContext> context) : base(context)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User {Id = 1, Username = "testuser", FirstName = "John", LastName = "Doe", Email = "johnny@test.com", Bio = "what up! new here", ProfileImageUrl = "", IsSeller = true, DateCreated = new DateTime().Date, Uid = "blah"},
                new User {Id = 2, Username = "broodski", FirstName = "Carl", LastName = "Cane", Email = "carlcane@test.com", Bio = "i love raisin canes", ProfileImageUrl = "", IsSeller = true, DateCreated = DateTime.Today, Uid = "blah"},
                new User {Id = 3, Username = "superbasic", FirstName = "Jack", LastName = "Black", Email = "jackblack@test.com", Bio = "I am Jackson Black", ProfileImageUrl = "", IsSeller = false, DateCreated = DateTime.Now, Uid = "blah"}
            });

            modelBuilder.Entity<Product>().HasData(new Product[]
            {
                new Product { Id = 1, Title = "Microwave", Description = "Heat it up bro", Price = 299.99M, ImageUrl = "blankfornow", IsAvailable = true, QuantityAvailable = 3, SellerId = 2, CategoryId = 1 },
                new Product { Id = 2, Title = "Keyboard", Description = "Type me up bro", Price = 149.99M, ImageUrl = "blankfornow", IsAvailable = true, QuantityAvailable = 2, SellerId = 1, CategoryId = 2 },
                new Product { Id = 3, Title = "Guitar", Description = "Strum me bro", Price = 649.99M, ImageUrl = "blankfornow", IsAvailable = false, QuantityAvailable = 0, SellerId = 2, CategoryId = 3 }
            });

            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category { Id = 1, Title = "Appliances" },
                new Category { Id = 2, Title = "Electronics" },
                new Category { Id = 3, Title = "Musical Instruments" }
            });

            modelBuilder.Entity<PaymentType>().HasData(new PaymentType[]
            {
                new PaymentType { Id = 1, Type = "Credit" },
                new PaymentType { Id = 2, Type = "Debit" },
                new PaymentType { Id = 3, Type = "PayPal" }
            });

            modelBuilder.Entity<Order>().HasData(new Order[]
            {
                new Order { Id = 1, CustomerId = 3, IsComplete = true, PaymentTypeId = 2 },
                new Order { Id = 2, CustomerId = 2, IsComplete = true, PaymentTypeId = 3 },
            });
        }
    }

}
