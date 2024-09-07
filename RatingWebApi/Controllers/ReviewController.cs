using Microsoft.AspNetCore.Mvc;
using Rating.Domain.Entties;
using Rating.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rating.WebApi.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // 1. POST: api/reviews - Add a new review
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] Reviews review)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _reviewRepository.AddReviewAsync(review);
            return Ok("Review added successfully.");
        }

        // 2. GET: api/reviews/{bookId} - Get all reviews for a specific book
        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetReviewsByBookId(int bookId)
        {
            var reviews = await _reviewRepository.GetReviewsByBookIdAsync(bookId);

            if (reviews == null || !reviews.Any())
                return NotFound("No reviews found for this book.");

            return Ok(new
            {
                BookId = bookId,
                Reviews = reviews
            });
        }

        // 3. GET: api/AvgRating/{bookId} - Get the average rating for a specific book
        [HttpGet("AvgRating/{bookId}")]
        public async Task<IActionResult> GetAverageRatingByBookId(int bookId)
        {
            var averageRating = await _reviewRepository.GetAverageRatingByBookIdAsync(bookId);

            if (averageRating == null)
                return NotFound("No average rating found for this book.");

            return Ok(new
            {
                BookId = bookId,
                AvgRating = averageRating.AvgRating,
                TotalReviews = averageRating.TotalReview
            });
        }
    }
}
