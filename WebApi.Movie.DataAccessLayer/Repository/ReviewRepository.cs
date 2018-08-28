namespace WebApi.Movie.DataAccessLayer.Repository
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebApi.Movie.DataAccessLayer.Interface;
    using WebApi.Movie.DataAccessLayer.Model;
    using System.Linq;

    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Review entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<Review>(entity);
            _context.Set<Review>().Add(entity);
        }      

        public void Edit(Review entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<Review>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public Review Find(int userId, int movieId)
        {
           return _context.Reviews
                         .Where(o => o.MovieId == movieId && o.UserId == userId)
                         .Select(o => o).FirstOrDefault();                   
        }        

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
