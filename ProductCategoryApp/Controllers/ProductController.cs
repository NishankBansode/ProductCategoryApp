using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductCategoryApp.Data;
using ProductCategoryApp.Models;

namespace ProductCategoryApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext db;
        public ProductController(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10; // Number of items per page
            var products = db.Products
                                   .Include(p => p.Category)
                                   .Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToList();

            int totalProducts = await db.Products.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = page;

            return View(products);
        }


        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!string.IsNullOrEmpty(product.ProductName) && product.CategoryId != 0)
            {
                db.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Categories"] = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await db.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductId) return NotFound();

            if (!string.IsNullOrEmpty(product.ProductName) && product.CategoryId != 0)
            {
                db.Update(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var product = await db.Products.Include(p => p.Category).FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
