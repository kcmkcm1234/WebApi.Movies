
namespace WebApi.Movie.DataAccessLayer.Repository
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using WebApi.Movie.DataAccessLayer.Interface;
    using WebApi.Movie.DataAccessLayer.Model;

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IMovieRepository _movie;
        private readonly IReviewRepository _review;
      
        public DatabaseInitializer( ApplicationDbContext context, RoleManager<ApplicationRole> roleManager,
          UserManager<ApplicationUser> userManager, IMovieRepository movie,IReviewRepository review )
        {
            _context = context;           
            _roleManager = roleManager;
            _userManager = userManager;
            _movie = movie;
            _review = review;
        }

        public void Seed()
        {
            CreateRoles();
            CreateUsers();
            CreateMovies();
            //CreateReviews();

           _context.SaveChanges();

           _context.Database.Migrate();
        }

        private void CreateRoles()
        {
            var rolesToAdd = new List<ApplicationRole>()
            {
                new ApplicationRole { Name= "Admin"},
                new ApplicationRole { Name= "User"}
            };

            foreach (var role in rolesToAdd)
            {
                if (!_roleManager.RoleExistsAsync(role.Name).Result)
                {
                    _roleManager.CreateAsync(role).Result.ToString();
                }
            }
        }

        private void CreateUsers()
        {
            if (!_context.ApplicationUser.Any())
            {
                var adminUser = new ApplicationUser { UserName = "admin@admin.com", FirstName = "Admin first", LastName = "Admin last", Email = "admin@admin.com"};
                _userManager.CreateAsync(adminUser, "P@ssw0rd!").Result.ToString();
                _userManager.AddToRoleAsync(_userManager.FindByNameAsync("admin@admin.com").GetAwaiter().GetResult(), "Admin").Result.ToString();

                var normalUser = new ApplicationUser { UserName = "user@user.com", FirstName = "First", LastName = "Last", Email = "user@user.com"};
                _userManager.CreateAsync(normalUser, "P@ssw0rd!").Result.ToString();
                _userManager.AddToRoleAsync(_userManager.FindByNameAsync("user@user.com").GetAwaiter().GetResult(), "User").Result.ToString();
            }
        }

        private void CreateMovies()
        {
            var movies = new List<Movie> {
                new Movie { Title="Olympus Has Fallen",  Genre="Action Adventure", YearOfRelease=2013, RunningTime = "30 minutes" },
                new Movie { Title="Man of Steel", Genre="Action Adventure", YearOfRelease=2013, RunningTime = "60 minutes"},
                new Movie { Title="Iron Man 3",  Genre="Action", YearOfRelease=2013, RunningTime = "120 minutes" },
            };

            if (!_context.Movies.Any())
            {
                _context.Movies.AddRange(movies);            
            }
        }

        private void CreateReviews()
        {
            var reviews = new List<Review> {
                new Review { MovieId = 1, UserId = 1, Rating = 5 },
                new Review { MovieId = 2, UserId = 1, Rating = 4},
                new Review { MovieId = 3, UserId = 1, Rating = 1 },
                new Review { MovieId = 1, UserId = 2, Rating = 2 },
                new Review { MovieId = 2, UserId = 2, Rating = 5 },
                new Review { MovieId = 3, UserId = 2, Rating = 4}                
            };

            if (!_context.Reviews.Any())
            {
                _context.Reviews.AddRange(reviews);
            }
        }

    }
}
