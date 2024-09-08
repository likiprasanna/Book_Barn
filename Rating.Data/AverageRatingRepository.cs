using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rating.Domain.Entties;
using Rating.Domain.Repositories;

namespace Rating.Data // this module is completly added
{
    public class AverageRatingRepository : IAverageRatingRepository
    {
        private readonly RatingDbContext _context;

        public AverageRatingRepository(RatingDbContext context)
        {
            _context = context;
        }

        public async Task UpdateAverageRatingAsync(int bookId, double avgRating, int totalReviews)
        {
            var avgRatingEntity = await _context.AverageRating.FirstOrDefaultAsync(a => a.BookId == bookId);

            if (avgRatingEntity == null)
            {
                avgRatingEntity = new AverageRating
                {
                    BookId = bookId,
                    AvgRating = avgRating,
                    TotalReview = totalReviews
                };
                _context.AverageRating.Add(avgRatingEntity);
            }
            else
            {
                avgRatingEntity.AvgRating = avgRating;
                avgRatingEntity.TotalReview = totalReviews;
                _context.AverageRating.Update(avgRatingEntity);
            }

            await _context.SaveChangesAsync();
        }
    }

}
