namespace WebApi.Movie.Service.Command
{
    using System.Collections.Generic;
    using WebApi.Movie.Service.ViewModel;

    public interface IUserAverageRatingsCommand : ICommand
    {
        List<MovieResponse> Response { get; set; }
    }
}
