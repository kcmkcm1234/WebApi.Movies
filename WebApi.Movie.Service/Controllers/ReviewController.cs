namespace WebApi.Movie.Service.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Movie.Service.Command;
    using WebApi.Movie.Service.ViewModel;

    [Route("api/review")]
    public class ReviewController : BaseController
    {
        private readonly IReviewCommand _reviewCommand;     
        public ReviewController(IReviewCommand reviewCommand)
        {
            _reviewCommand = reviewCommand;          
        }
               
        [HttpPost]      
        public IActionResult Post([FromBody]ReviewRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(request);
            }

            _reviewCommand.Request = request;
            _reviewCommand.Execute();

           if(!_reviewCommand.Match)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
