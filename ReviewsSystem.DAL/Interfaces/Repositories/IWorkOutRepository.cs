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
    public interface IWorkOutRepository : IRepository<WorkOut>
    {
        Task<PagedList<WorkOut>> GetAsync(WorkOutsParameters parameters);
    }
}
