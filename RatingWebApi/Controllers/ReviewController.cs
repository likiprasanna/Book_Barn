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
        private readonly IAverageRatingRepository _averageRatingRepository;//a

        public ReviewsController(IReviewRepository reviewRepository, IAverageRatingRepository averageRatingRepository)
        {
            _reviewRepository = reviewRepository;
            _averageRatingRepository = averageRatingRepository;
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

        //from here it starts
        [HttpPut("{reviewId}")]
        public async Task<IActionResult> EditReview(int reviewId, [FromBody] Reviews updatedReview)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = await _reviewRepository.GetReviewByIdAsync(reviewId);
            if (review == null)
                return NotFound("Review not found.");

            // Update review details
            review.Review = updatedReview.Review;
            review.Rating = updatedReview.Rating;

            await _reviewRepository.UpdateReviewAsync(review);
            await UpdateAverageRating(review.BookId); // Update average rating after editing a review

            return Ok("Review updated successfully.");
        }

        // 5. DELETE: api/reviews/{reviewId} - Delete a review
        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(reviewId);
            if (review == null)
                return NotFound("Review not found.");

            await _reviewRepository.DeleteReviewAsync(review);
            await UpdateAverageRating(review.BookId); // Update average rating after deleting a review

            return Ok("Review deleted successfully.");
        }

        // Helper method to update average rating for a book
        private async Task UpdateAverageRating(int bookId)
        {
            var reviews = await _reviewRepository.GetReviewsByBookIdAsync(bookId);
            if (reviews == null || !reviews.Any())
            {
                // No reviews found, set AvgRating to 0 and TotalReviews to 0
                await _averageRatingRepository.UpdateAverageRatingAsync(bookId, 0, 0);
            }
            else
            {
                // Calculate the new average rating
                var avgRating = reviews.Average(r => r.Rating);
                var totalReviews = reviews.Count();
                await _averageRatingRepository.UpdateAverageRatingAsync(bookId, avgRating, totalReviews);
            }
        }

        // GET: api/reviews/user/{userId}/book/{bookId} - Get review by userId and bookId
        [HttpGet("user/{userId}/book/{bookId}")]
        public async Task<IActionResult> GetReviewByUserAndBook(int userId, int bookId)
        {
            var review = await _reviewRepository.GetReviewByUserAndBookIdAsync(userId, bookId);

            if (review == null)
                return NotFound("No review found for this user and book.");

            return Ok(review);
        }

        // PUT: api/reviews/user/{userId}/book/{bookId} - Edit a review by userId and bookId
        [HttpPut("user/{userId}/book/{bookId}")]
        public async Task<IActionResult> EditReviewByUserAndBook(int userId, int bookId, [FromBody] Reviews updatedReview)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = await _reviewRepository.GetReviewByUserAndBookIdAsync(userId, bookId);
            if (review == null)
                return NotFound("Review not found for this user and book.");

            // Update review details
            review.Review = updatedReview.Review;
            review.Rating = updatedReview.Rating;

            await _reviewRepository.UpdateReviewAsync(review);
            await UpdateAverageRating(bookId); // Update average rating after editing a review

            return Ok("Review updated successfully.");
        }

        // DELETE: api/reviews/user/{userId}/book/{bookId} - Delete a review by userId and bookId
        [HttpDelete("user/{userId}/book/{bookId}")]
        public async Task<IActionResult> DeleteReviewByUserAndBook(int userId, int bookId)
        {
            var review = await _reviewRepository.GetReviewByUserAndBookIdAsync(userId, bookId);
            if (review == null)
                return NotFound("Review not found for this user and book.");

            await _reviewRepository.DeleteReviewAsync(review);
            await UpdateAverageRating(bookId); // Update average rating after deleting a review

            return Ok("Review deleted successfully.");
        }


    }
}
