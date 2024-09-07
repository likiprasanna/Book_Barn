using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rating.Data;
using Rating.Domain.Entties;
using Rating.Domain.Repositories;

namespace Rating.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly RatingDbContext _context;

        public ReviewRepository(RatingDbContext context)
        {
            _context = context;
        }

        public async Task AddReviewAsync(Reviews review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();

            // After adding a review, update the average rating for the book
            var averageRating = await _context.AverageRating.FirstOrDefaultAsync(a => a.BookId == review.BookId);
            if (averageRating == null)
            {
                averageRating = new AverageRating
                {
                    BookId = review.BookId,
                    AvgRating = review.Rating,
                    TotalReview = 1
                };
                await _context.AverageRating.AddAsync(averageRating);
            }
            else
            {
                averageRating.TotalReview += 1;
                averageRating.AvgRating = ((averageRating.AvgRating * (averageRating.TotalReview - 1)) + review.Rating) / averageRating.TotalReview;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reviews>> GetReviewsByBookIdAsync(int bookId)
        {
            return await _context.Reviews
                .Where(r => r.BookId == bookId)
                .ToListAsync();
        }

        public async Task<AverageRating> GetAverageRatingByBookIdAsync(int bookId)
        {
            return await _context.AverageRating
                .FirstOrDefaultAsync(a => a.BookId == bookId);
        }
    }
}
