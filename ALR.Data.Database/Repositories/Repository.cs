using ALR.Data.Database.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ALR.Data.Database.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        ALRDBContext _dbContext;

        public Repository(ALRDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteEntityAsync(T entity)
        {
            EntityEntry entry = _dbContext.Entry<T>(entity);
            entry.State = EntityState.Deleted;
        }

        public void DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var entities = _dbContext.Set<T>().Where(expression).ToList();
            if (entities.Count > 0)
            {
                _dbContext.Set<T>().RemoveRange(entities);
            }
        }

        public async Task<T> GetByIDAsync(object id)
        {
            return await _dbContext.Set<T>().FirstAsync((Expression<Func<T, bool>>)id);
        }
        public async Task<T> GetByConditionAsync(Expression<Func<T, bool>> expression = null)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(expression);
        }
        public async Task<T> GetByConditionIncludeAsync(Expression<Func<T, object>> expressionInclude1 = null, Expression<Func<T, object>> expressionInclude2 = null, Expression<Func<T, bool>> expression = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (expressionInclude1 != null)
            {
                query = query.Include(expressionInclude1);
            }
            if (expressionInclude2 != null)
            {
                query = query.Include(expressionInclude2);
            }

            if (expression != null)
            {
                query = query.Where(expression);
            }

            T result = await query.FirstOrDefaultAsync();

            return result;
        }
        public async Task<List<T>> GetDataDoubleIncludeAsync(Expression<Func<T, object>> expressionInclude1 = null, Expression<Func<T, object>> expressionInclude2 = null, Expression<Func<T, bool>> expression = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (expressionInclude1 != null)
            {
                query = query.Include(expressionInclude1);
            }
            if (expressionInclude2 != null)
            {
                query = query.Include(expressionInclude2);
            }
            if(expression != null)
            {
                query = query.Where(expression);

            }
            List<T> result = await query.ToListAsync();

            return result;
        }
        public async Task<List<T>> GetListByConditionIncludeThenIncludeAsync(Expression<Func<T, bool>> expression,
                                                                               Func<IQueryable<T>, IQueryable<T>> includeBuilder)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (includeBuilder != null)
            {
                query = includeBuilder(query);
            }

            return await query.Where(expression).ToListAsync();
        }

        public async Task<T> GetByConditionIncludeThenIncludeAsync(Expression<Func<T, bool>> expression,
                                                                               Func<IQueryable<T>, IQueryable<T>> includeBuilder)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (includeBuilder != null)
            {
                query = includeBuilder(query);
            }

            T result = await query.Where(expression).FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<T>> GetDataAsync(Expression<Func<T, bool>> expression = null)
        {
            try
            {
                if (expression == null)
                {
                    return await _dbContext.Set<T>().ToListAsync();
                }
                return await _dbContext.Set<T>().Where(expression).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void InsertAsync(T entity)
        {

            EntityEntry entry =  _dbContext.Set<T>().Add(entity);
            entry.State = EntityState.Added;
            
        }

        public async Task InsertRangeAsync(IEnumerable<T> entity)
        {
            await _dbContext.Set<T>().AddRangeAsync(entity);
        }

        public async Task CommitChangeAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Xử lý lỗi khi lưu thay đổi vào cơ sở dữ liệu
                // Đối với DbUpdateException, bạn có thể truy cập vào InnerException để biết lỗi chi tiết
                throw new Exception("Lỗi khi lưu thay đổi vào cơ sở dữ liệu.", ex.InnerException);
            }
        }

        public void UpdateAsync(T entity)
        {
            EntityEntry entry = _dbContext.Entry<T>(entity);
            entry.State = EntityState.Modified;
        }

        public async Task<IEnumerable<T>> GetDataIncludeAsync<TProperty>(Expression<Func<T, bool>> expression, Expression<Func<T, TProperty>> includePath1, Expression<Func<TProperty, object>> includePath2 = null)
        {
            if (expression == null)
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
            IIncludableQueryable<T, TProperty> query = _dbContext.Set<T>().Where(expression).AsNoTracking().Include(includePath1);
            if (includePath2 != null)
            {
                var test = query.ThenInclude(includePath2);
                return await test.ToListAsync();
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetOnlyDataIncludeAsync(Expression<Func<T, object>> expression = null, Expression<Func<T, bool>> expression1 = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (expression != null)
            {
                query = query.Include(expression);
            }

            if (expression1 != null)
            {
                query = query.Where(expression1);
            }

            IEnumerable<T> result = await query.ToListAsync();

            return result;
        }
    }
}
