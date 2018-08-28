namespace WebApi.Movie.DataAccessLayer.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using WebApi.Movie.DataAccessLayer.Model;
    using WebApi.Movie.DataAccessLayer.Interface;

    public class MovieUserRepository : IMovieUserRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Movie> GetRatingByUser(int userId)
        {
            return _context.Reviews
                .Where(o => o.UserId == userId)
                .OrderByDescending(o=>o.Rating)
                         .Select(o => o.Movie).ToList();
        }

        public IEnumerable<Movie> GetRatingByUsers()
        {
            return _context.Reviews
                 .OrderByDescending(o => o.Rating)
                          .Select(o => o.Movie).ToList();
        }
    }
}
