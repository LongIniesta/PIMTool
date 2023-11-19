using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Database;

namespace PIMTool.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly PIMDBContext _pimContext;
        private readonly DbSet<T> _set;

        public Repository(PIMDBContext pimContext)
        {
            _pimContext = pimContext;
            _set = _pimContext.Set<T>();
        }

        public IQueryable<T> Get()
        {
            return _set.Where(x => true);
        }

        public async Task<T?> GetAsync(decimal id, CancellationToken cancellationToken = default)
        {
            return await Get().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _set.AddRangeAsync(entities, cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _set.AddAsync(entity, cancellationToken);
        }

        public void Delete(params T[] entities)
        {
            _set.RemoveRange(entities);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _pimContext.SaveChangesAsync(cancellationToken);
        }
    }
}