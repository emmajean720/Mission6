using Microsoft.AspNetCore.Mvc;
using Mission6.Data;
using Mission6.Models;

namespace Mission6.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieRepository _movieRepository;

        public MoviesController()
        {
            _movieRepository = new MovieRepository();
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _movieRepository.AddMovie(movie);
                return RedirectToAction("Index", "Home");
            }
            return View(movie);
        }
        public IActionResult MovieList()
        {
            var movies = _movieRepository.GetAllMovies();
            return View(movies);
        }

    }
}

