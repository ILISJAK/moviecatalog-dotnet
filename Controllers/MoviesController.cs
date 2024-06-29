using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Data;
using MovieCatalog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieCatalogContext _context;

        public MoviesController(MovieCatalogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies([FromQuery] string genre = null, [FromQuery] string sortOption = null)
        {
            var moviesQuery = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(genre))
            {
                moviesQuery = moviesQuery.Where(m => m.Genre.Contains(genre));
            }

            moviesQuery = sortOption switch
            {
                "title" => moviesQuery.OrderBy(m => m.Title),
                "releaseDate" => moviesQuery.OrderBy(m => m.ReleaseDate),
                "rating" => moviesQuery.OrderBy(m => m.Rating),
                _ => moviesQuery
            };

            return await moviesQuery.ToListAsync();
        }
    }
}
