namespace WebApi.Movie.Service.Command
{
    using System.Collections.Generic;
    using WebApi.Movie.DataAccessLayer.Interface;
    using WebApi.Movie.DataAccessLayer.Model;
    using WebApi.Movie.Service.ViewModel;
    using System.Linq;
    public class MovieFilterCommand : IMovieFilterCommand
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IRatingCommand _ratingCommand;
        public MovieFilterCommand(IMovieRepository movieRepository, IRatingCommand ratingCommand)
        {
            _movieRepository = movieRepository;
            _ratingCommand = ratingCommand;
        }
        public MovieRequest Request { get; set; }
        public List<MovieResponse> Response { get; set; }
        public bool Match { get; set; }
        public bool Valid { get; set; }

        public void Execute()
        {
            ValidateFilters();

            if(Valid)
            {
                Response = new List<MovieResponse>();
                Match = false;
                
                var movies = _movieRepository.GetAll();

                movies = ApplyFilter(movies);

                if (movies.Any())
                {
                    foreach (var movie in movies)
                    {
                        _ratingCommand.Reviews = movie.Reviews;
                        _ratingCommand.Execute();

                        var response = new MovieResponse()
                        {
                            Id = movie.Id,
                            Genre = movie.Genre,
                            Title = movie.Title,
                            YearOfRelease = movie.YearOfRelease,
                            RunningTime = movie.RunningTime,
                            AverageRating = _ratingCommand.AverageRating
                        };

                        Response.Add(response);
                    }

                    Match = true;
                }              
            }          
        }

        #region private

        private void ValidateFilters()
        {
            Valid = true;

            if (string.IsNullOrEmpty(Request.Genre) && string.IsNullOrEmpty(Request.Title) && Request.YearOfRelease == 0)
            {
                Valid = false;
            }
           
        }
        private List<Movie> ApplyFilter(List<Movie> movies)
        {
            if(!string.IsNullOrEmpty(Request.Genre))
            {
                movies = movies.Where(x => x.Genre.Contains(Request.Genre)).ToList();
            }

            if (!string.IsNullOrEmpty(Request.Title))
            {
                movies = movies.Where(x => x.Title.Contains(Request.Title)).ToList();
            }

            if (Request.YearOfRelease > 0)
            {
                movies = movies.Where(x => x.YearOfRelease == Request.YearOfRelease).ToList();
            }

            return movies;
        }

        #endregion


    }
}
