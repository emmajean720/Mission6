using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mission6.Data;
using Mission6.Models;

namespace Mission6.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieRepository movieRepository;
        public MoviesController(IConfiguration configuration)
        {
            movieRepository = new MovieRepository(configuration);
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
                movieRepository.AddMovie(movie);
                return RedirectToAction("Index", "Home");
            }
            return View(movie);
        }

        // GET: Movies/MovieList
        public IActionResult MovieList()
        {
            var movies = movieRepository.GetAllMovies();
            return View(movies);
        }

        // GET: Edit
        public IActionResult Edit(int id)
        {
            var movie = movieRepository.GetAllMovies().FirstOrDefault(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound(); // or RedirectToAction("MovieList");
            }
            return View(movie);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                movieRepository.UpdateMovie(movie);
                return RedirectToAction("MovieList");
            }
            return View(movie);
        }

        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            movieRepository.DeleteMovie(id);
            return RedirectToAction("MovieList");
        }
    }
}
