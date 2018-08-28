namespace WebApi.Movie.Service.ViewModel
{
    using System.ComponentModel.DataAnnotations;

    public class ReviewRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
