using ReviewsSystem.DAL.Interfaces;
using ReviewsSystem.DAL.Interfaces.Repositories;

namespace ReviewsSystem.DAL.Data
{
    public class UnitOfWork : IUnitOfWork

    {
        protected readonly ReviewsSystemContext _context;
        public IReviewRepository ReviewRepository { get; }


        public IWorkOutRepository WorkOutRepository { get; }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public UnitOfWork(ReviewsSystemContext context,
            IReviewRepository reviewRepository, 
            IWorkOutRepository workOutRepository)
        {
            _context = context;
            ReviewRepository = reviewRepository;
            WorkOutRepository = workOutRepository;
        }
    }
}
