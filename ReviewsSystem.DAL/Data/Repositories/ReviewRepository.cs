using Microsoft.EntityFrameworkCore;
using ReviewsSystem.DAL.Entities;
using ReviewsSystem.DAL.Exceptions;
using ReviewsSystem.DAL.Interfaces.Repositories;
using ReviewsSystem.DAL.Pagination;
using ReviewsSystem.DAL.Parameters;

namespace ReviewsSystem.DAL.Data.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ReviewsSystemContext context) 
            : base(context)
        {
        }

        public override async Task<Review> GetCompleteEntityAsync(int id)
        {
            var review = await table.Include(r => r.WorkOut)  
                                     .Include(r => r.User)    
                                     .SingleOrDefaultAsync(r => r.Id == id);

            return review ?? throw new EntityNotFoundException(
                GetEntityNotFoundErrorMessage(id));
        }

        public async Task<PagedList<Review>> GetAsync(ReviewsParameters parameters)
        {
            IQueryable<Review> source = table.Include(r => r.WorkOut)
                                             .Include(r => r.User);

            SearchByUserId(ref source, parameters.UserId);
            SearchByWorkoutId(ref source, parameters.WorkOutId);
            SearchByRating(ref source, parameters.Rating);

            return await PagedList<Review>.ToPagedListAsync(
                source,
                parameters.PageNumber,
                parameters.PageSize);
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(int userId)
        {
            return await table.Include(r => r.User)
                              .Where(r => r.UserId == userId)
                              .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsForWorkoutAsync(int workoutId)
        {
            return await table.Include(r => r.WorkOut)
                              .Where(r => r.WorkOutId == workoutId)
                              .ToListAsync();
        }

        private static void SearchByUserId(ref IQueryable<Review> source, int? userId)
        {
            if (userId.HasValue && userId > 0)
            {
                source = source.Where(review => review.UserId == userId.Value);
            }
        }

        private static void SearchByWorkoutId(ref IQueryable<Review> source, int? workoutId)
        {
            if (workoutId.HasValue && workoutId > 0)
            {
                source = source.Where(review => review.WorkOutId == workoutId.Value);
            }
        }

        private static void SearchByRating(ref IQueryable<Review> source, int? rating)
        {
            if (rating.HasValue && rating >= 1 && rating <= 5) 
            {
                source = source.Where(review => review.Rating == rating.Value);
            }
        }

    }
}
