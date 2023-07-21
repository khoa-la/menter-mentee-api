using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace _2MC.DataTier.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        IQueryable<TEntity> Get();
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        TEntity Get<TKey>(TKey id);
        Task<TEntity> GetAsync<TKey>(TKey id);
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);
        // void AddRange(IEnumerable<TEntity> entities);
        // Task AddRangeAsyn(IEnumerable<TEntity> entities);
        // void RemoveRange(IEnumerable<TEntity> entities);
        // void UpdateRange(List<TEntity> entity);
        // Task CreateAsyn(TEntity entity);
        // TEntity FirstOrDefault();
        // TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        // Task<TEntity> FirstOrDefaultAsyn();
        // Task<TEntity> FirstOrDefaultAsyn(Expression<Func<TEntity, bool>> predicate);
        // Task<TEntity> GetAsyn<TKey>(TKey id);
        // Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default);
        // int Count();
    }
}