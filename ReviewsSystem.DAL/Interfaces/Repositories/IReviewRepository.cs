using ReviewsSystem.DAL.Entities;
using ReviewsSystem.DAL.Pagination;
using ReviewsSystem.DAL.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsSystem.DAL.Interfaces.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<PagedList<Review>> GetAsync(ReviewsParameters parameters);

        Task<IEnumerable<Review>> GetReviewsByUserIdAsync(int userId);

        Task<IEnumerable<Review>> GetReviewsForWorkoutAsync(int workoutId);
    }
}
