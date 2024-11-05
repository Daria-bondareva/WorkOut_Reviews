using ReviewsSystem.DAL.Entities;
using ReviewsSystem.DAL.Interfaces.Repositories;
using ReviewsSystem.DAL.Pagination;
using ReviewsSystem.DAL.Parameters;

namespace ReviewsSystem.DAL.Data.Repositories
{
    public class WorkOutRepository : GenericRepository<WorkOut>, IWorkOutRepository
    {
        public WorkOutRepository(ReviewsSystemContext context) : base(context)
        {
        }

        public Task<PagedList<WorkOut>> GetAsync(WorkOutsParameters parameters)
        {
            throw new NotImplementedException();
        }

        public override Task<WorkOut> GetCompleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
