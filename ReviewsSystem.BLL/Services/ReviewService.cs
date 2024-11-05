using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReviewsSystem.BLL.DTO.Requests;
using ReviewsSystem.BLL.DTO.Responses;
using ReviewsSystem.BLL.Interfaces;
using ReviewsSystem.DAL;
using ReviewsSystem.DAL.Entities;
using ReviewsSystem.DAL.Pagination;
using ReviewsSystem.DAL.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewsSystem.BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ReviewsSystemContext _context;
        private readonly IMapper _mapper;

        public ReviewService(ReviewsSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewResponse>> GetAsync()
        {
            var reviews = await _context.Reviews.ToListAsync();
            return _mapper.Map<IEnumerable<ReviewResponse>>(reviews);
        }

        public async Task<PagedList<ReviewResponse>> GetAllAsync(ReviewsParameters parameters)
        {
            var query = _context.Reviews.AsQueryable();

            if (parameters.UserId.HasValue)
            {
                query = query.Where(r => r.UserId == parameters.UserId.Value);
            }

            if (parameters.WorkOutId.HasValue)
            {
                query = query.Where(r => r.WorkOutId == parameters.WorkOutId.Value);
            }

            if (parameters.Rating.HasValue)
            {
                query = query.Where(r => r.Rating == parameters.Rating.Value);
            }

            var reviews = await PagedList<Review>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize);
            var response = _mapper.Map<IEnumerable<ReviewResponse>>(reviews);

            return new PagedList<ReviewResponse>(response, reviews.TotalEntitiesCount, reviews.CurrentPage, reviews.PageSize);
        }

        public async Task<ReviewResponse> GetByIdAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            return _mapper.Map<ReviewResponse>(review);
        }

        public async Task InsertAsync(ReviewRequest request)
        {
            var review = _mapper.Map<Review>(request);
            review.DatePosted = DateTime.UtcNow; 
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ReviewRequest request)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _mapper.Map(request, review);
                _context.Reviews.Update(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ReviewResponse>> GetByUserIdAsync(int userId)
        {
            var reviews = await _context.Reviews
                                         .Where(r => r.UserId == userId)
                                         .ToListAsync();

            return _mapper.Map<IEnumerable<ReviewResponse>>(reviews);
        }

        public async Task<IEnumerable<ReviewResponse>> GetByWorkOutIdAsync(int workOutId)
        {
            var reviews = await _context.Reviews
                                         .Where(r => r.WorkOutId == workOutId)
                                         .ToListAsync();

            return _mapper.Map<IEnumerable<ReviewResponse>>(reviews);
        }
    }
}
