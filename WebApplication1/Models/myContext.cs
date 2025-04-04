using Microsoft.EntityFrameworkCore;
namespace Ecommerce.Models;

#pragma warning disable CA1050 // Declare types in namespaces
public class MyContext : DbContext
#pragma warning restore CA1050 // Declare types in namespaces
{
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {
        
    }
}