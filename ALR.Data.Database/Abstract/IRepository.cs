using System.Linq.Expressions;

namespace ALR.Data.Database.Abstract
{
    public interface IRepository<T> where T : class
    {
        void DeleteAsync(Expression<Func<T, bool>> expression);
        void DeleteEntityAsync(T entity);
        Task<T> GetByConditionAsync(Expression<Func<T, bool>> expression = null);
        Task<T> GetByIDAsync(object id);
        Task<IEnumerable<T>> GetDataAsync(Expression<Func<T, bool>> expression = null);
        Task InsertRangeAsync(IEnumerable<T> entity);
        void InsertAsync(T entity);
        Task CommitChangeAsync();
        void UpdateAsync(T entity);
        Task<T> GetByConditionIncludeAsync(Expression<Func<T, object>> expressionInclude1 = null, Expression<Func<T, object>> expressionInclude2 = null, Expression<Func<T, bool>> expression = null);
        Task<IEnumerable<T>> GetDataIncludeAsync<TProperty>(Expression<Func<T, bool>> expression, Expression<Func<T, TProperty>> includePath1, Expression<Func<TProperty, object>> includePath2 = null);
        Task<IEnumerable<T>> GetOnlyDataIncludeAsync(Expression<Func<T, object>> expression = null, Expression<Func<T, bool>> expression1 = null);
        Task<List<T>> GetListByConditionIncludeThenIncludeAsync(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IQueryable<T>> includeBuilder);
        Task<T> GetByConditionIncludeThenIncludeAsync(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IQueryable<T>> includeBuilder);
        Task<List<T>> GetDataDoubleIncludeAsync(Expression<Func<T, object>> expressionInclude1 = null, Expression<Func<T, object>> expressionInclude2 = null, Expression<Func<T, bool>> expression = null);
    }
}
