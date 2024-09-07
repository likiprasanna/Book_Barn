using System.Collections.Generic;
using System.Threading.Tasks;
using Rating.Domain.Entties;

namespace Rating.Domain.Repositories
{
    public interface IReviewRepository
    {
        Task AddReviewAsync(Reviews review);
        Task<IEnumerable<Reviews>> GetReviewsByBookIdAsync(int bookId);
        Task<AverageRating> GetAverageRatingByBookIdAsync(int bookId);
    }
}
