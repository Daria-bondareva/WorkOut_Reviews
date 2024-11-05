using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReviewsSystem.BLL.DTO.Requests;
using ReviewsSystem.BLL.DTO.Responses;
using ReviewsSystem.BLL.Interfaces;
using ReviewsSystem.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewsSystem.DAL;
using ReviewsSystem.DAL.Pagination;
using ReviewsSystem.DAL.Parameters;

namespace ReviewsSystem.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly ReviewsSystemContext _context;
        private readonly IMapper _mapper;

        public UserService(ReviewsSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponse>> GetAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }

        public async Task<PagedList<UserResponse>> GetAllAsync(UsersParameters parameters)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.UserName))
            {
                query = query.Where(u => u.UserName.Contains(parameters.UserName));
            }

            if (!string.IsNullOrEmpty(parameters.Email))
            {
                query = query.Where(u => u.Email.Contains(parameters.Email));
            }

            var users = await PagedList<User>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize);
            var response = _mapper.Map<IEnumerable<UserResponse>>(users);

            return new PagedList<UserResponse>(response, users.TotalEntitiesCount, users.CurrentPage, users.PageSize);
        }

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> GetByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return _mapper.Map<UserResponse>(user);
        }

        public async Task InsertAsync(UserRequest request)
        {
            var user = _mapper.Map<User>(request);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UserRequest request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _mapper.Map(request, user);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserResponse>> GetByUserIdAsync(int userId)
        {
            var user = await _context.Users.Include(u => u.Reviews)
                                           .FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                return _mapper.Map<IEnumerable<UserResponse>>(user.Reviews);
            }
            return new List<UserResponse>();
        }

        public async Task<IEnumerable<UserResponse>> GetByWorkOutIdAsync(int workOutId)
        {
            var reviews = await _context.Reviews
                                        .Include(r => r.User)
                                        .Where(r => r.WorkOutId == workOutId)
                                        .ToListAsync();

            var users = reviews.Select(r => r.User).Distinct(); 
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }
    }
}
