namespace WebApi.Movie.DataAccessLayer.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebApi.Movie.DataAccessLayer.Interface;
    using WebApi.Movie.DataAccessLayer.Model;
    using System.Linq;

    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Movie> GetAll()
        {
            return _context.Movies.ToList();                                            
        }

        public Movie GetById(int movieId)
        {
            return _context.Movies
                        .Where(o => o.Id == movieId)
                        .Select(o => o).FirstOrDefault();
        }
    }
}
