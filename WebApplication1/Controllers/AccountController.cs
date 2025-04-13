using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

public class AccountController : Controller {
    private readonly AppDbContext db;

    public AccountController(AppDbContext context) => db = context;

    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(string email, string password) {
        var admin = db.Admins.FirstOrDefault(a => a.Email == email && a.Password == password);
        if (admin != null) {
            HttpContext.Session.SetString("AdminEmail", admin.Email);
            return RedirectToAction("Dashboard", "Admin");
        }

        var customer = db.Customers.FirstOrDefault(c => c.Email == email && c.Password == password);
        if (customer != null) {
            HttpContext.Session.SetString("CustomerEmail", customer.Email);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Message = "Invalid login credentials.";
        return View();
    }

    public IActionResult Register() => View();

    [HttpPost]
    public IActionResult Register(Customer customer) {
        if (ModelState.IsValid) {
            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
        return View(customer);
    }

    public IActionResult Logout() {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
