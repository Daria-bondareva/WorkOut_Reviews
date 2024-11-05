using ReviewsSystem.BLL.DTO.Requests;
using ReviewsSystem.BLL.DTO.Responses;
using ReviewsSystem.DAL.Pagination;
using ReviewsSystem.DAL.Parameters;

namespace ReviewsSystem.BLL.Interfaces
{
    public interface IWorkOutService
    {
        Task<IEnumerable<WorkOutResponse>> GetAsync(); 


        Task<PagedList<WorkOutResponse>> GetAllAsync(WorkOutsParameters parameters);
        Task<WorkOutResponse> GetByIdAsync(int id); 

        Task InsertAsync(WorkOutRequest request); 

        Task UpdateAsync(int id, WorkOutRequest request); 

        Task DeleteAsync(int id);
        Task<IEnumerable<ReviewResponse>> GetByUserIdAsync(int userId); 

        Task<IEnumerable<ReviewResponse>> GetByWorkOutIdAsync(int workOutId);
    }
}
