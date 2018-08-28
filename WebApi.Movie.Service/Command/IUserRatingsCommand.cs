namespace WebApi.Movie.Service.Command
{
    using System.Collections.Generic;
    using WebApi.Movie.Service.ViewModel;

    public interface IUserRatingsCommand : ICommand
    {
        int UserId { get; set; }
        List<MovieResponse> Response { get; set; }
    }
}
