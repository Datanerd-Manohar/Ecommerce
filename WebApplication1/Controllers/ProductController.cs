using Microsoft.AspNetCore.Mvc;

public class ProductController: Controller 
{
    
    private readonly AppDbContext db;

    public ProductController(AppDbContext context) => db = context;

    public IActionResult Index() => View(db.Products.ToList());

    public IActionResult Add() => View();

   [HttpPost]
    public IActionResult Add(Product product) {
    if (!ModelState.IsValid)
        return View(product);
    
    try {
        db.Products.Add(product);
        db.SaveChanges();
        return RedirectToAction("Index");
    }
    catch (Exception ex) {
        ViewBag.Error = "Error: " + ex.Message;
        return View(product);
    }
    }
    public IActionResult Edit(int id)
    {
        var product = db.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    // POST: /Product/Edit
    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            db.Products.Update(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(product);
    }

    [HttpPost]
    public IActionResult Delete(int[] selectedIds)
    {
    if (selectedIds != null && selectedIds.Length > 0)
    {
        var productsToDelete = db.Products.AsEnumerable().Where(p => selectedIds.Contains(p.ProductId)).ToList(); 
        db.Products.RemoveRange(productsToDelete);
        db.SaveChanges();
    }

    return RedirectToAction("Index");
    }

}
 