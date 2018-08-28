namespace WebApi.Movie.Service.Command
{
    using Microsoft.AspNetCore.Identity;
    using WebApi.Movie.DataAccessLayer.Interface;
    using WebApi.Movie.DataAccessLayer.Model;
    using WebApi.Movie.Service.ViewModel;

    public class ReviewCommand : IReviewCommand
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewCommand(IReviewRepository reviewRepository, 
            IMovieRepository movieRepository, UserManager<ApplicationUser> userManager)
        {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
            _userManager = userManager;
        }     
        public bool Match { get; set; }
        public ReviewRequest Request { get; set; }
      
        public void Execute()
        {
            ValidateRequest();

            if(Match)
            {
                var review = _reviewRepository.Find(Request.UserId, Request.MovieId);

                if(review != null)
                {
                    review.MovieId = Request.MovieId;
                    review.UserId = Request.UserId;
                    review.Rating = Request.Rating;

                    _reviewRepository.Edit(review);
                }
                else
                {
                    var newReview = new Review()
                    {
                        MovieId = Request.MovieId,
                        UserId = Request.UserId,
                        Rating = Request.Rating
                    };

                    _reviewRepository.Add(newReview);                   
                }

                _reviewRepository.Commit();
            }
        }

        #region private

        private async void ValidateRequest()
        {
            Match = true;

            var userManager = await _userManager.FindByIdAsync(Request.UserId.ToString());
            var move = _movieRepository.GetById(Request.MovieId);

           if (userManager == null || move == null)
            {
                Match = false;
            }        
        }

        #endregion
    }
}
