namespace WebApi.Movie.DataAccessLayer.Interface
{
    using System.Collections.Generic;
    using WebApi.Movie.DataAccessLayer.Model;

   public interface IMovieRepository 
   {
        List<Movie> GetAll();
        Movie GetById(int movieId);
   }
}
