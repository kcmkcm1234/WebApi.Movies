namespace WebApi.Movie.Service.ViewModel
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public string Genre { get; set; }    
        public string RunningTime { get; set; }
        public double AverageRating { get; set; }
    }
}
