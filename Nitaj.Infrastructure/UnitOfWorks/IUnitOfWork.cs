using Nitaj.Domain.Interfaces;

namespace Nitaj.Infrastructure.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task CommitAsync();
        Task RollbackAsync();
    }
}
