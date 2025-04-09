using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication1.Models;

public class ProductsController : Controller
{
    private readonly AppDbContext db;

    public ProductsController(AppDbContext context)
    {
        db = context;
    }

    public IActionResult Index()
    {
        var products = db.Products.ToList();
        return View(products);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Product product)
    {
        if (ModelState.IsValid)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(product);
    }
}
