namespace WebApi.Movie.Service.Command
{
    using System.Collections.Generic;
    using WebApi.Movie.DataAccessLayer.Interface;
    using WebApi.Movie.Service.ViewModel;
    using System.Linq;

    public class UserAverageRatingsCommand : IUserAverageRatingsCommand
    {
        private readonly IMovieUserRepository _movieUserRepository;
        private readonly IRatingCommand _ratingCommand;

        public UserAverageRatingsCommand(IMovieUserRepository movieUserRepository, IRatingCommand ratingCommand)
        {
            _movieUserRepository = movieUserRepository;
            _ratingCommand = ratingCommand;
        }
        public List<MovieResponse> Response { get; set; }

        public void Execute()
        {
            var movieResponse = new List<MovieResponse>();          
            var draw = new HashSet<double>();

            var movies = _movieUserRepository.GetRatingByUsers();

            foreach (var movie in movies)
            {
                _ratingCommand.Reviews = movie.Reviews;
                _ratingCommand.Execute();

                var response = new MovieResponse()
                {
                    Genre = movie.Genre,
                    Title = movie.Title,
                    RunningTime = movie.RunningTime,
                    YearOfRelease = movie.YearOfRelease,
                    AverageRating = _ratingCommand.AverageRating
                };

                movieResponse.Add(response);
                draw.Add(response.AverageRating);
            }

            if ((movieResponse.Count - draw.Count) >= 1)
            {
                Response = movieResponse.OrderBy(o => o.Title).Take(5).ToList();
            }
            else
            {
                Response = movieResponse.OrderByDescending(o=>o.AverageRating).Take(5).ToList();
            }
        }       
    }
}
