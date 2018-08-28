namespace WebApi.Movie.Service.Command
{
    using System.Collections.Generic;
    using WebApi.Movie.Service.ViewModel;

    public interface IMovieFilterCommand : ICommand
    {
        bool Match { get; set; }
        bool Valid { get; set; }
        MovieRequest Request { get; set; }
        List<MovieResponse> Response { get; set; }
    }
}
