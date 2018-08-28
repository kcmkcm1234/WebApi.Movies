namespace WebApi.Movie.Service.Command
{
    using System.Collections.Generic;
    using WebApi.Movie.DataAccessLayer.Model;
    using System.Linq;
    using System;

    public class RatingCommand : IRatingCommand
    {
        public ICollection<Review> Reviews { get; set; }
        public double AverageRating { get; set; }

        public void Execute()
        {
            var ratingScores = from o in Reviews
                               group o by o.MovieId into rating
                               select new
                               {
                                   scores = rating.Sum(x => x.Rating),
                                   count = rating.Count()
                               };

            double totalScore = ratingScores.Sum(x => x.scores);
            double totalRating = ratingScores.Sum(x => x.count);

            double averageRating = totalScore / totalRating;

            if(averageRating > 0)
            {
                AverageRating = Math.Round(averageRating * 2, MidpointRounding.AwayFromZero) / 2;
            }
        }       
    }
}
