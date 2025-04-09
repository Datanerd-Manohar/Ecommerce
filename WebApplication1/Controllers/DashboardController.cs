using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AdminController : Controller
{
    private readonly AppDbContext db;

    public AdminController(AppDbContext context)
    {
        db = context;
    }

    public IActionResult Dashboard()
    {
        var viewModel = new AdminDashboardViewModel
        {
            Customers = db.Customers.ToList(),
            Products = db.Products.ToList(),
            Orders = db.Orders.ToList()
        };

        return View(viewModel);
    }
}
