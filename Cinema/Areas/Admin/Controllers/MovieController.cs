using Cinema.DataAcess;
using Cinema.Models;
using Cinema.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
namespace Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private ApplicationDbContext _context = new();



        public IActionResult Index()
        {
            var movie = _context.Movies.AsNoTracking().AsQueryable();
            return View(movie.Select(e => new MovieVM
            {
                Id = e.Id,
                Description = e.Description,
                Name = e.Name,
                Price = e.Price,
                Status = e.Status,
                categoryname = e.Category.Name,
                cinemaname = e.Cinemaa.Name,
            }).AsQueryable());
        }
        [HttpGet]
        public IActionResult Create()
        {

            var cinemaas = _context.Cinemaas.AsNoTracking().AsQueryable();
            var categories = _context.Categories.AsNoTracking().AsQueryable();


            return View(new MovieCreateVM()
            {
                Categories = categories.AsEnumerable(),
                Cinemaas = cinemaas.AsEnumerable(),
            });

        }

        [HttpPost]
        public IActionResult Create(Movie movie, IFormFile MainImg, List<IFormFile> Subimgs)
        {
            if (MainImg is not null && MainImg.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(MainImg.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", fileName);

                //if(!System.IO.File.Exists(filePath))
                //{
                //    System.IO.File.Create(filePath);
                //}

                using (var stream = System.IO.File.Create(filePath))
                {
                    MainImg.CopyTo(stream);
                }

                movie.MainImg = fileName;
            }
            var MovieCreated=_context.Movies.Add(movie);
            _context.SaveChanges();

            if (Subimgs is not null && Subimgs.Count > 0)
            {
                foreach(var item in Subimgs)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\Movie_Subimg", fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        item.CopyTo(stream);
                    }
                    _context.MovieSubimgs.Add(new()
                    {
                        Image = fileName,
                        MovieId = MovieCreated.Entity.Id,
                    });
                }
                _context.SaveChanges();

            }
           
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.Find(id);

            if (movie is null)
                return RedirectToAction("NotFoundPage", "Home");
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Cinemaas = new SelectList(_context.Cinemaas, "Id", "Name");


            return View(new MovieCreateVM()
            {
                Movie = movie,
                Categories = _context.Categories.AsNoTracking().AsEnumerable(),
                Cinemaas = _context.Cinemaas.AsNoTracking().AsEnumerable(),
                MovieSubimgs = _context.MovieSubimgs.AsNoTracking().Where(e=>e.MovieId==id),

            });
        }

        [HttpPost]
        public IActionResult Edit(Movie movie, IFormFile file)
        {
            var movieInDB = _context.Movies.AsNoTracking().FirstOrDefault(e => e.Id == movie.Id);

            if (movieInDB is null)
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

                    movie.MainImg = fileName;
                }
            }
            else
            {
                movie.MainImg = movieInDB.MainImg;
            }
            _context.Movies.Update(movie);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.Find(id);

            if (movie is null)
                return RedirectToAction("NotFoundPage", "Home");

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
