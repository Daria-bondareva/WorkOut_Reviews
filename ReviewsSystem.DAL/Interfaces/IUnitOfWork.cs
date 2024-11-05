using ReviewsSystem.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsSystem.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IReviewRepository ReviewRepository { get; }
        IWorkOutRepository WorkOutRepository { get; }
        Task SaveChangesAsync();
    }
}
