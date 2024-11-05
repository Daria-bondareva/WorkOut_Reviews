using ReviewsSystem.BLL.DTO.Requests;
using ReviewsSystem.BLL.DTO.Responses;
using ReviewsSystem.DAL.Pagination;
using ReviewsSystem.DAL.Parameters;

namespace ReviewsSystem.BLL.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponse>> GetAsync(); 
        Task<PagedList<ReviewResponse>> GetAllAsync(ReviewsParameters parameters);

        Task<ReviewResponse> GetByIdAsync(int id); 

        Task InsertAsync(ReviewRequest request); 

        Task UpdateAsync(int id, ReviewRequest request); 

        Task DeleteAsync(int id);
        Task<IEnumerable<ReviewResponse>> GetByUserIdAsync(int userId); 

        Task<IEnumerable<ReviewResponse>> GetByWorkOutIdAsync(int workOutId); 
    }
}
