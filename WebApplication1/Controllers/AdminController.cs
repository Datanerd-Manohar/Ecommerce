using Microsoft.AspNetCore.Mvc;
namespace WebApplication1.Controllers;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return RedirectToAction("Dashboard", "Admin");
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var admin = _context.Admins.FirstOrDefault(a => a.Email == email && a.Password == password);
        if (admin != null)
        {
            // You can set session or authentication logic here
            return RedirectToAction("Dashboard"); // Redirect to your admin dashboard
        }

        ViewBag.Error = "Invalid email or password.";
        return View();
    }
}
