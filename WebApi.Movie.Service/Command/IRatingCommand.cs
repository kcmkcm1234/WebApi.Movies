namespace WebApi.Movie.Service.Command
{
    using System.Collections.Generic;
    using WebApi.Movie.DataAccessLayer.Model;

    public interface IRatingCommand : ICommand
    {       
        ICollection<Review> Reviews { get; set; }
        double AverageRating { get; set; }
    }
}
