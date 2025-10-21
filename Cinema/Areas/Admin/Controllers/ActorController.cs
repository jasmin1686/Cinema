using Cinema.DataAcess;
using Cinema.Models;
using Cinema.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ActorController : Controller
    {
        private ApplicationDbContext _context = new();


        public IActionResult Index()
        {
            var actors = _context.Actors
                .Include(a => a.Actormovies)
                .ThenInclude(am => am.Movie)
                .AsNoTracking()
                .ToList();

            return View(actors);
        }



        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Movies = _context.Movies.AsNoTracking().ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ActorVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Movies = _context.Movies.ToList();
                return View(vm);
            }

            var actor = new Actor
            {
                Name = vm.Name
            };

           
            if (vm.Img != null && vm.Img.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(vm.Img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Actors", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                using (var stream = System.IO.File.Create(filePath))
                {
                    vm.Img.CopyTo(stream);
                }

                actor.Img = fileName;
            }
            if (vm.SelectedMovies != null)
            {
                actor.Actormovies = vm.SelectedMovies
                    .Select(mid => new Actormovie { MovieId = mid, Actor = actor })
                    .ToList();
            }

            _context.Actors.Add(actor);
            _context.SaveChanges();



            return RedirectToAction(nameof(Index));
        }

       
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var actor = _context.Actors.Find(id);

            if (actor is null)
                return RedirectToAction("NotFoundPage", "Home");

            return View(actor);
        }

        
        [HttpPost]
        public IActionResult Edit(Actor actor, IFormFile ImgFile)
        {
            var actorInDB = _context.Actors.AsNoTracking().FirstOrDefault(a => a.Id == actor.Id);

            if (actorInDB is null)
                return RedirectToAction("NotFoundPage", "Home");

            if (ImgFile is not null && ImgFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\Actors", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ImgFile.CopyTo(stream);
                }

                actor.Img = fileName;
            }
            else
            {
                actor.Img = actorInDB.Img;
            }

            _context.Actors.Update(actor);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Delete(int id)
        {
            var actor = _context.Actors.Find(id);

            if (actor is null)
                return RedirectToAction("NotFoundPage", "Home");

            _context.Actors.Remove(actor);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
