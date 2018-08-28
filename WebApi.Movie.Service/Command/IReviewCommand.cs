namespace WebApi.Movie.Service.Command
{
    using WebApi.Movie.Service.ViewModel;

    public interface IReviewCommand : ICommand
    {
        bool Match { get; set; }      
        ReviewRequest Request { get; set; }      
    }
}
