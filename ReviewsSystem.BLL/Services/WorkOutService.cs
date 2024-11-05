using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReviewsSystem.BLL.DTO.Requests;
using ReviewsSystem.BLL.DTO.Responses;
using ReviewsSystem.BLL.Interfaces;
using ReviewsSystem.DAL.Entities;
using ReviewsSystem.DAL;
using ReviewsSystem.DAL.Pagination;
using ReviewsSystem.DAL.Parameters;

namespace ReviewsSystem.BLL.Services
{
    public class WorkOutService : IWorkOutService
    {
        private readonly ReviewsSystemContext _context;
        private readonly IMapper _mapper;

        public WorkOutService(ReviewsSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WorkOutResponse>> GetAsync()
        {
            var workouts = await _context.WorkOuts.ToListAsync();
            return _mapper.Map<IEnumerable<WorkOutResponse>>(workouts);
        }

        public async Task<PagedList<WorkOutResponse>> GetAllAsync(WorkOutsParameters parameters)
        {
            var query = _context.WorkOuts.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Name))
            {
                query = query.Where(w => w.Name.Contains(parameters.Name));
            }

            if (parameters.TypeId.HasValue)
            {
                query = query.Where(w => w.TypeId == parameters.TypeId.Value);
            }

            if (parameters.MaxPrice.HasValue)
            {
                query = query.Where(w => w.Price <= parameters.MaxPrice.Value);
            }

            var workouts = await PagedList<WorkOut>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize);
            var response = _mapper.Map<IEnumerable<WorkOutResponse>>(workouts);

            return new PagedList<WorkOutResponse>(response, workouts.TotalEntitiesCount, workouts.CurrentPage, workouts.PageSize);
        }

        public async Task<WorkOutResponse> GetByIdAsync(int id)
        {
            var workout = await _context.WorkOuts.FindAsync(id);
            return _mapper.Map<WorkOutResponse>(workout);
        }

        public async Task InsertAsync(WorkOutRequest request)
        {
            var workout = _mapper.Map<WorkOut>(request);
            _context.WorkOuts.Add(workout);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, WorkOutRequest request)
        {
            var workout = await _context.WorkOuts.FindAsync(id);
            if (workout != null)
            {
                _mapper.Map(request, workout);
                _context.WorkOuts.Update(workout);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var workout = await _context.WorkOuts.FindAsync(id);
            if (workout != null)
            {
                _context.WorkOuts.Remove(workout);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ReviewResponse>> GetByUserIdAsync(int userId)
        {
            var workouts = await _context.WorkOuts
                                         .Include(w => w.Reviews)
                                         .ThenInclude(r => r.User)
                                         .ToListAsync();

            var userReviews = workouts.SelectMany(w => w.Reviews
                                                       .Where(r => r.UserId == userId));

            return _mapper.Map<IEnumerable<ReviewResponse>>(userReviews);
        }

        public async Task<IEnumerable<ReviewResponse>> GetByWorkOutIdAsync(int workOutId)
        {
            var reviews = await _context.Reviews
                                         .Where(r => r.WorkOutId == workOutId)
                                         .Include(r => r.User)
                                         .ToListAsync();

            return _mapper.Map<IEnumerable<ReviewResponse>>(reviews);
        }
    }
}
