using Cinema.DataAcess;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CinemaController : Controller
    {
        private ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            var cinema = _context.Cinemaas.AsNoTracking().AsQueryable();
            return View(cinema.AsEnumerable());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cinemaa cinemaa, IFormFile file)
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

                cinemaa.ImgPath = fileName;
            }

            _context.Cinemaas.Add(cinemaa);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
       
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var brand = _context.Cinemaas.Find(id);

            if (brand is null)
                return RedirectToAction("NotFoundPage", "Home");

            return View(brand);
        }

        [HttpPost]
        public IActionResult Edit(Cinemaa cinemaa, IFormFile file)
        {
            var brandInDB = _context.Cinemaas.AsNoTracking().FirstOrDefault(e => e.Id == cinemaa.Id);

            if (brandInDB is null)
                return RedirectToAction("NotFoundPage", "Home");

            if (file is not null)
            {
                if (file.Length > 0)
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

                    cinemaa.ImgPath = fileName;
                }
            }
            else
            {
                cinemaa.ImgPath = brandInDB.ImgPath;
            }
            _context.Cinemaas.Update(cinemaa);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var cinemaa = _context.Cinemaas.Find(id);

            if (cinemaa is null)
                return RedirectToAction("NotFoundPage", "Home");

            _context.Cinemaas.Remove(cinemaa);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
