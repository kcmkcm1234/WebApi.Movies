namespace WebApi.Movie.DataAccessLayer.Model
{
    using WebApi.Movie.DataAccessLayer.Interface;

    public class Review : EntityBase
    {
        public double Rating { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }       
    }
}
