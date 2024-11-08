using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCategoryApp.Data;
using ProductCategoryApp.Models;
using ProductCategoryApp.Services;

namespace ProductCategoryApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        
          public async Task<IActionResult> Index(int page=1)
        {
            int pageSize = 10;

            var categories = await _categoryService.GetPaginatedCategoriesAsync(page,pageSize);
            int totalCatogories = await _categoryService.GetTotalCategoriesCountAsync();
            int totalPages = (int)Math.Ceiling((double)totalCatogories / pageSize);


           
            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = page;
            return View(categories);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {

            await _categoryService.AddCategoryAsync(category);
            return RedirectToAction("Index");


        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var category = await _categoryService.GetCategoryByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Category category)
        {

           await _categoryService.UpdateCategoryAsync(category);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategoryByIdAsync(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            await _categoryService.DeleteCategoryAsync(id);

            return RedirectToAction("Index");
        }
    }
}





