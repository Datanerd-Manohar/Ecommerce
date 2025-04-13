using Microsoft.AspNetCore.Mvc;

public class CustomerController : Controller {
    private readonly AppDbContext db;

    public CustomerController(AppDbContext context) => db = context;

    
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(Customer customer)
    {
        if (ModelState.IsValid)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
        return View(customer);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var customer = db.Customers.FirstOrDefault(c => c.Email == email && c.Password == password);
        if (customer != null)
        {
            HttpContext.Session.SetString("CustomerEmail", email);
            return RedirectToAction("Dashboard");
        }

        ViewBag.Message = "Invalid credentials";
        return View();
    }

    public IActionResult Dashboard()
    {
        var email = HttpContext.Session.GetString("CustomerEmail");
        if (email == null) return RedirectToAction("Login");

        var customer = db.Customers.FirstOrDefault(c => c.Email == email);
        return View(customer);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("CustomerEmail");
        return RedirectToAction("Login");
    }
    
    
    public IActionResult Index() => View(db.Customers.ToList());

    public IActionResult Add() => View();

    [HttpPost]
    public IActionResult Add(Customer customer) {
        if (ModelState.IsValid) {
            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(customer);
    }

    public IActionResult Edit(int id) {
        var customer = db.Customers.Find(id);
        if (customer == null) return NotFound();
        return View(customer);
    }

    [HttpPost]
    public IActionResult Edit(Customer customer) {
        if (ModelState.IsValid) {
            db.Customers.Update(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(customer);
    }

    [HttpPost]
    public IActionResult Delete(List<int> selectedIds) {
        var customers = db.Customers
                  .AsEnumerable()
                  .Where(c => selectedIds.Contains(c.Id))
                  .ToList();
        db.Customers.RemoveRange(customers);
        db.SaveChanges();
        return RedirectToAction("Index");
    }
}