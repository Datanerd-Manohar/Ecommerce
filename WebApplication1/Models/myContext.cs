using Microsoft.EntityFrameworkCore;
namespace Ecommerce.Models;

#pragma warning disable CA1050 // Declare types in namespaces
public class MyContext : DbContext
#pragma warning restore CA1050 // Declare types in namespaces
{
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {
        
    }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
}