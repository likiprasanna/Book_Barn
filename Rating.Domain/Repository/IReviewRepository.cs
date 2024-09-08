using System.Collections.Generic;
using System.Threading.Tasks;
using Rating.Domain.Entties;

namespace Rating.Domain.Repositories
{
    public interface IReviewRepository
    {
        Task<Reviews> GetReviewByUserAndBookIdAsync(int userId, int bookId);//a
        Task AddReviewAsync(Reviews review);
        Task<IEnumerable<Reviews>> GetReviewsByBookIdAsync(int bookId);
        Task<Reviews> GetReviewByIdAsync(int reviewId);//a
        Task UpdateReviewAsync(Reviews review);//a
        Task DeleteReviewAsync(Reviews review);//a
        Task<AverageRating> GetAverageRatingByBookIdAsync(int bookId);
    }
    public interface IAverageRatingRepository//a
    {
        Task UpdateAverageRatingAsync(int bookId, double avgRating, int totalReviews);//a
    }
}
