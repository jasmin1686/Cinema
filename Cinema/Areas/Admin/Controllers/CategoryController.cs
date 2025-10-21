using Cinema.DataAcess;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            var categories = _context.Categories.AsNoTracking().AsQueryable();
            return View(categories.AsEnumerable());
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category ,IFormFile file)
        {
            if (file is not null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//Images", fileName);

                //if(!System.IO.File.Exists(filePath))
                //{
                //    System.IO.File.Create(filePath);
                //}

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                category.ImagePath = fileName;
            }

            
            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);

            if (category is null)
                return RedirectToAction("NotFoundPage", "Home");

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);

            if (category is null)
                return RedirectToAction("NotFoundPage", "Home");

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
