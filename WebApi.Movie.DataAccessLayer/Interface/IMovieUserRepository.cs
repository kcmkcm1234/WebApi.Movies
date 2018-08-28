namespace WebApi.Movie.DataAccessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using WebApi.Movie.DataAccessLayer.Model;
    public interface IMovieUserRepository
    {
        IEnumerable<Movie> GetRatingByUsers();
        IEnumerable<Movie> GetRatingByUser(int userId);
    }
}
