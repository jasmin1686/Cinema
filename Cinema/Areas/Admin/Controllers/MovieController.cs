using Cinema.Models;
using Cinema.Repository;
using Cinema.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cinema.Repository.IRepository;

//Movie Controller
//using Cinema.Models;
//using Cinema.Repository;
//using Cinema.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Cinemaa> _cinemaRepository;
        private readonly IRepository<Movie> _movieRepository;
        private readonly IMovieImgRepository _movieImgRepository;

        public MovieController(
            IRepository<Category> categoryRepository,
            IRepository<Cinemaa> cinemaRepository,
            IRepository<Movie> movieRepository,
            IMovieImgRepository movieImgRepository)
        {
            _categoryRepository = categoryRepository;
            _cinemaRepository = cinemaRepository;
            _movieRepository = movieRepository;
            _movieImgRepository = movieImgRepository;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
        
            var movies = await _movieRepository.GetAsync(tracked: false, cancellationToken: cancellationToken);

            
            var vm = movies.Select(e => new MovieVM
            {
                Id = e.Id,
                Description = e.Description,
                Name = e.Name,
                Price = e.Price,
                Status = e.Status,
                categoryname = e.Category?.Name,
                cinemaname = e.Cinemaa?.Name
            });

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var cinemaas = await _cinemaRepository.GetAsync(cancellationToken: cancellationToken);
            var categories = await _categoryRepository.GetAsync(cancellationToken: cancellationToken);

            return View(new MovieCreateVM
            {
                Categories = categories,
                Cinemaas = cinemaas
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movie movie, IFormFile MainImg, List<IFormFile> Subimgs, CancellationToken cancellationToken)
        {
            if (MainImg is not null && MainImg.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(MainImg.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                    await MainImg.CopyToAsync(stream, cancellationToken);

                movie.MainImg = fileName;
            }

            var movieCreated = await _movieRepository.CreateAsync(movie, cancellationToken);
            await _movieRepository.commitAsync(cancellationToken);

            if (Subimgs is not null && Subimgs.Count > 0)
            {
                foreach (var item in Subimgs)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Movie_Subimg", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                        await item.CopyToAsync(stream, cancellationToken);

                    await _movieImgRepository.CreateAsync(new MovieSubimg
                    {
                        Image = fileName,
                        MovieId = movieCreated.Id
                    }, cancellationToken);
                }
                await _movieImgRepository.commitAsync(cancellationToken);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetoneAsync(e => e.Id == id, cancellationToken: cancellationToken);
            if (movie is null)
                return RedirectToAction("NotFoundPage", "Home");

            ViewBag.Categories = new SelectList(
                await _categoryRepository.GetAsync(cancellationToken: cancellationToken), "Id", "Name", movie.CategoryId);

            ViewBag.Cinemaas = new SelectList(
                await _cinemaRepository.GetAsync(cancellationToken: cancellationToken), "Id", "Name", movie.CinemaId);

            return View(new MovieCreateVM
            {
                Movie = movie,
                Categories = await _categoryRepository.GetAsync(cancellationToken: cancellationToken),
                Cinemaas = await _cinemaRepository.GetAsync(cancellationToken: cancellationToken),
                MovieSubimgs = (IEnumerable<MovieSubimg>)await _movieImgRepository.GetAsync(e => e.MovieId == id, cancellationToken: cancellationToken)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Movie movie, IFormFile? file, CancellationToken cancellationToken)
        {
            var movieInDB = await _movieRepository.GetoneAsync(e => e.Id == movie.Id, cancellationToken: cancellationToken);
            if (movieInDB is null)
                return RedirectToAction("NotFoundPage", "Home");

            if (file is not null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                    await file.CopyToAsync(stream, cancellationToken);

                movie.MainImg = fileName;
            }
            else
            {
                movie.MainImg = movieInDB.MainImg;
            }

            _movieRepository.Update(movie);
            await _movieRepository.commitAsync(cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetoneAsync(e => e.Id == id, cancellationToken: cancellationToken);
            if (movie is null)
                return RedirectToAction("NotFoundPage", "Home");

            _movieRepository.Delete(movie);
            await _movieRepository.commitAsync(cancellationToken);

            return RedirectToAction(nameof(Index));
        }
    }
}
