using System.Linq.Expressions;

namespace Nitaj.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T Add(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
        T Find(Expression<Func<T, bool>> criteria, string[] includes = null);
        T Update(T entity);
        void Delete(T entity);
    }
}
