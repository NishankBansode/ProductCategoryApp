using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductCategoryApp.Data;
using ProductCategoryApp.Models;
using ProductCategoryApp.Services;


namespace ProductCategoryApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) 
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10; // Number of items per page
            var products = await _productService.GetPaginatedProductAsync(page, pageSize);

            int totalProducts = await _productService.GetTotalProductCountAsync();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = page;

            return View(products);
        }


        public async Task<IActionResult> Create()
        {
            ViewData["Categories"] = new SelectList(await _productService.GetCategoriesAsync(), "CategoryId", "CategoryName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!string.IsNullOrEmpty(product.ProductName) && product.CategoryId != 0)
            {
                await _productService.AddProductAsync(product);
                return RedirectToAction("Index");
            }
            ViewData["Categories"] = new SelectList(await _productService.GetCategoriesAsync(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(await _productService.GetCategoriesAsync(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductId) return NotFound();

            if (!string.IsNullOrEmpty(product.ProductName) && product.CategoryId != 0)
            {
               await _productService.UpdateProductAsync(product);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(await _productService.GetCategoriesAsync(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
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
           await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
