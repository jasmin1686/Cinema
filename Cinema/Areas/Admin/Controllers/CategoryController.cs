using Cinema.DataAcess;
using Cinema.Models;
using Cinema.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        // private ApplicationDbContext _context = new();
        private Repository<Category> _categoryRepository ;
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAsync(tracked:false, cancellationToken: cancellationToken);
            return View(categories.AsEnumerable());
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category ,IFormFile file,CancellationToken cancellationToken)
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

            
          await _categoryRepository.CreateAsync(category, cancellationToken);
           
          await  _categoryRepository.commitAsync(cancellationToken);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id,CancellationToken cancellationToken)
        {
            var category =await _categoryRepository.GetoneAsync(e=>e.Id ==id, cancellationToken: cancellationToken);

            if (category is null)
                return RedirectToAction("NotFoundPage", "Home");

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category ,CancellationToken cancellationToken)
        {
            _categoryRepository.Update(category);
          await  _categoryRepository.commitAsync(cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var category =await _categoryRepository.GetoneAsync(e => e.Id == id, cancellationToken: cancellationToken);

            if (category is null)
                return RedirectToAction("NotFoundPage", "Home");

           _categoryRepository.Delete(category);
           await _categoryRepository.commitAsync(cancellationToken);

            return RedirectToAction(nameof(Index));
        }
    }
}
