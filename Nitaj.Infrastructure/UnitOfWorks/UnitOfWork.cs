using Nitaj.Domain.Interfaces;
using Nitaj.Infrastructure.Context;
using Nitaj.Infrastructure.Repositories;

namespace Nitaj.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly NitajDbContext _context;
        private readonly Dictionary<string, object> _repositories;
        private bool _disposed;

        public UnitOfWork(NitajDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repositories = new Dictionary<string, object>();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var entityType = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(entityType))
            {
                var repositoryType = typeof(GenericRepository<TEntity>);
                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
                _repositories.Add(entityType, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[entityType];
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            _context.ChangeTracker.Clear();
            await Task.CompletedTask;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
