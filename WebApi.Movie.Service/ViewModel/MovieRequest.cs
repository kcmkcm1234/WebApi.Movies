namespace WebApi.Movie.Service.ViewModel
{
    using System.ComponentModel.DataAnnotations;

    public class MovieRequest
    {
        [StringLength(100)]
        public string Title { get; set; }
        //[Range(0, 2025)]
        public int YearOfRelease { get; set; }
        [StringLength(100)]
        public string Genre { get; set; }      
    }
}
