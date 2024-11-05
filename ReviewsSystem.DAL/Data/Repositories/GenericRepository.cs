using ReviewsSystem.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using ReviewsSystem.DAL.Exceptions;


namespace ReviewsSystem.DAL.Data.Repositories
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ReviewsSystemContext _context;
        protected readonly DbSet<TEntity> table;

        public GenericRepository(ReviewsSystemContext context)
        {
            _context = context;
            table = _context.Set<TEntity>();
        }


        public virtual async Task<IEnumerable<TEntity>> GetAsync() => await table.ToListAsync();

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await table.FindAsync(id)
                ?? throw new EntityNotFoundException(
                    GetEntityNotFoundErrorMessage(id));
        }

        public abstract Task<TEntity> GetCompleteEntityAsync(int id);

        public virtual async Task InsertAsync(TEntity entity)
        {
            await table.AddAsync(entity);
            await _context.SaveChangesAsync();
        }


        public virtual async Task UpdateAsync(TEntity entity)
        {
            table.Update(entity);
            await _context.SaveChangesAsync();
        }


        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            await Task.Run(() => table.Remove(entity));
            await _context.SaveChangesAsync();
        }

        protected static string GetEntityNotFoundErrorMessage(int id) =>
            $"{typeof(TEntity).Name} with id {id} not found.";

    }
}
