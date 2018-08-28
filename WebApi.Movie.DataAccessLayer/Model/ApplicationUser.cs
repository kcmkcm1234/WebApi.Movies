namespace WebApi.Movie.DataAccessLayer.Model
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
