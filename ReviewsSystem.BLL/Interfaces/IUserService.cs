using ReviewsSystem.BLL.DTO.Requests;
using ReviewsSystem.BLL.DTO.Responses;
using ReviewsSystem.DAL.Pagination;
using ReviewsSystem.DAL.Parameters;

namespace ReviewsSystem.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAsync();
        Task<PagedList<UserResponse>> GetAllAsync(UsersParameters parameters);

        Task<UserResponse> GetByIdAsync(int id); 

        Task<UserResponse> GetByEmailAsync(string email); 

        Task InsertAsync(UserRequest request); 

        Task UpdateAsync(int id, UserRequest request); 

        Task DeleteAsync(int id); 
        Task<IEnumerable<UserResponse>> GetByUserIdAsync(int userId); 

        Task<IEnumerable<UserResponse>> GetByWorkOutIdAsync(int workOutId);
    }
}
