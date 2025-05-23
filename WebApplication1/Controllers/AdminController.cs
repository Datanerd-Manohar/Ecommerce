using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Linq;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(Admin admin)
    {
        var existingAdmin = _context.Admins
            .FirstOrDefault(a => a.Email == admin.Email && a.Password == admin.Password);

        if (existingAdmin != null)
        {
            HttpContext.Session.SetString("AdminEmail", existingAdmin.Email);
            return RedirectToAction("Dashboard");
        }

        TempData["Error"] = "Invalid email or password.";
        return View(admin);
    }

    
    public IActionResult Dashboard()
    {
    var email = HttpContext.Session.GetString("AdminEmail");
    if (email == null) return RedirectToAction("Login");

    var admin = _context.Admins.FirstOrDefault(a => a.Email == email);

    var viewModel = new AdminDashboardViewModel
    {
        Admin = admin,
        Customers = _context.Customers.ToList(),
        Products = _context.Products.ToList(),
        Orders = _context.Orders.ToList()
    };

    return View(viewModel);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    public IActionResult EditOrder(int id)
    {
    var order = _context.Orders.Find(id);
    if (order == null) return NotFound();
    return View(order);
    }

    [HttpPost]
    public IActionResult EditOrder(Order updatedOrder)
    {
    if (ModelState.IsValid)
    {
        _context.Orders.Update(updatedOrder);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }
    return View(updatedOrder);

    }

    [HttpPost]
    public IActionResult DeleteOrder(int id)
    {
    var order = _context.Orders.Find(id);
    if (order != null)
    {
        _context.Orders.Remove(order);
        _context.SaveChanges();
    }
    return RedirectToAction("Dashboard");
    }

}
