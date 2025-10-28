using Cinema.DataAcess;
using Cinema.Models;
using Cinema.Repository;
using Cinema.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CinemaController : Controller
    {
        //private ApplicationDbContext _context = new();
        private IRepository<Cinemaa>_cinemaRepository ;
        public CinemaController(IRepository<Cinemaa> cinemaRepository)
        {
            _cinemaRepository = cinemaRepository;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var cinema = await _cinemaRepository.GetAsync(cancellationToken: cancellationToken);
            return View(cinema.AsEnumerable());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cinemaa cinemaa, IFormFile file,CancellationToken cancellationToken)
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

            await _cinemaRepository.CreateAsync(cinemaa, cancellationToken);

            await _cinemaRepository.commitAsync(cancellationToken);

            return RedirectToAction(nameof(Index));
        }
       
        [HttpGet]
        public async Task<IActionResult> Edit(int id,CancellationToken cancellationToken)
        {
            var brand = await _cinemaRepository.GetoneAsync(e => e.Id == id, cancellationToken: cancellationToken);


            if (brand is null)
                return RedirectToAction("NotFoundPage", "Home");

            return View(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Cinemaa cinemaa, IFormFile file,CancellationToken cancellationToken)
        {
            var brandInDB = await _cinemaRepository.GetoneAsync(e => e.Id == cinemaa.Id, cancellationToken: cancellationToken);

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
            _cinemaRepository.Update(cinemaa);
            await _cinemaRepository.commitAsync(cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken)
        {
            var cinemaa = await _cinemaRepository.GetoneAsync(e => e.Id == id, cancellationToken: cancellationToken);


            if (cinemaa is null)
                return RedirectToAction("NotFoundPage", "Home");

            _cinemaRepository.Delete(cinemaa);
            await _cinemaRepository.commitAsync(cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
