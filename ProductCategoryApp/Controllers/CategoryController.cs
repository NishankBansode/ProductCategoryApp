using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCategoryApp.Data;
using ProductCategoryApp.Models;

namespace ProductCategoryApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db;
        public CategoryController(ApplicationDbContext _db) 
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }
        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category) 
        {
          
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
          
            
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
           
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
          
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = db.Categories
                .FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            db.Categories.Remove(category);
            
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}





