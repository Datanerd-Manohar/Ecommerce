using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

public class OrdersController : Controller
{
    private readonly AppDbContext db;

    public OrdersController(AppDbContext context)
    {
        db = context;
    }

    public IActionResult Index()
    {
        var orders = db.Orders.Include(o => o.Customer).ToList();
        return View(orders);
    }

    public IActionResult Details(int id)
    {
        var order = db.Orders
                      .Include(o => o.OrderItems)
                      .ThenInclude(oi => oi.Product)
                      .Include(o => o.Customer)
                      .FirstOrDefault(o => o.Id == id);

        if (order == null) return NotFound();
        return View(order);
    }

    public IActionResult Delete(int id)
    {
        var order = db.Orders.Find(id);
        if (order == null) return NotFound();
        return View(order);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var order = db.Orders.Find(id);
        db.Orders.Remove(order);
        db.SaveChanges();
        return RedirectToAction("Index");
    }
}
