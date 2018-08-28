namespace WebApi.Movie.Service.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Movie.DataAccessLayer.Interface;
    using WebApi.Movie.Service.Command;
    using WebApi.Movie.Service.ViewModel;

    [Route("api/movie")]
    public class MovieController : BaseController
    {
        private readonly IMovieFilterCommand _movieFilterCommand;
        private readonly IUserRatingsCommand _userRatingsCommand;
        private readonly IUserAverageRatingsCommand _userAverageRatingsCommand;
        public MovieController(IMovieRepository movieRepository, IMovieUserRepository movieUserRepository,
            IUserRatingsCommand userRatingsCommand, IMovieFilterCommand movieFilterCommand,
            IUserAverageRatingsCommand userAverageRatingsCommand)
        {
            _movieFilterCommand = movieFilterCommand;
            _userRatingsCommand = userRatingsCommand;
            _userAverageRatingsCommand = userAverageRatingsCommand;
        }
       
        [HttpGet]
        [Route("filtermovies")]
        public IActionResult GetFilter(MovieRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }

            _movieFilterCommand.Request = request;
            _movieFilterCommand.Execute();

            if(!_movieFilterCommand.Valid)
            {
                return BadRequest();
            }

            if (!_movieFilterCommand.Match)
            {
                return NotFound();
            }           

            return Ok(_movieFilterCommand.Response);
        }

        [HttpGet]
        [Route("moviesbyusers")]
        public IActionResult GetMoviesByUsers()
        {
            _userAverageRatingsCommand.Execute();

            return Ok(_userAverageRatingsCommand.Response);
        }

        [HttpGet]
        [Route("moviesbyuser/{userId:int}")]
        public IActionResult GeMoviesByUser(int userId)
        {
            _userRatingsCommand.UserId = userId;
            _userRatingsCommand.Execute();

            return Ok(_userRatingsCommand.Response);
        }
    }
}
