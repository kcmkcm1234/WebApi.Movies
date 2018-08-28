namespace WebApi.Movie.DataAccessLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebApi.Movie.DataAccessLayer.Interface;

    public class Movie : EntityBase
    {
        public string Title {get;set;}
        public int YearOfRelease { get; set; } 
        public string Genre { get; set; }
        public string RunningTime { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
