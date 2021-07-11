using System.Linq;
using Application.Domain.Data;
using Application.Domain.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.WWW.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IRepository<IMovie> _movieRepository;

        public MoviesController(ILogger<MoviesController> logger, IRepository<IMovie> movieRepository)
        {
            _logger = logger;
            _movieRepository = movieRepository;
        }

        public IActionResult Index()
        {
            var movies = _movieRepository.FindAll();
            
            return View(movies.ToList());
        }
    }
}