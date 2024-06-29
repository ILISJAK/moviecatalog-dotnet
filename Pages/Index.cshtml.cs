using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Data;
using MovieCatalog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MovieCatalogContext _context;

        public IndexModel(MovieCatalogContext context)
        {
            _context = context;
        }

        public IList<Movie> BannerMovies { get; set; } = new List<Movie>();
        public IList<Movie> FeaturedMovies { get; set; } = new List<Movie>();
        public IList<Movie> RecommendedMovies { get; set; } = new List<Movie>();

        public async Task OnGetAsync()
        {
            BannerMovies = await GetRandomMoviesAsync(3);
            FeaturedMovies = await GetRandomMoviesAsync(4);
            RecommendedMovies = await GetRandomMoviesAsync(4);
        }

        private async Task<IList<Movie>> GetRandomMoviesAsync(int count)
        {
            var movies = await _context.Movies.ToListAsync();
            var random = new System.Random();
            return movies.OrderBy(x => random.Next()).Take(count).ToList();
        }
    }
}
