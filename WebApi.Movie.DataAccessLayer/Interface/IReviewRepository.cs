namespace WebApi.Movie.DataAccessLayer.Interface
{
    using WebApi.Movie.DataAccessLayer.Model;

    public interface IReviewRepository 
    {
        Review Find(int userId, int movieId);
        void Add(Review entity);
        void Edit(Review entity);       
        void Commit();
    }
}
