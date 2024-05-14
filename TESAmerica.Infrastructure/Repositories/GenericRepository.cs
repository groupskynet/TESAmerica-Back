using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TESAmerica.Application.Contracts.Persistence;
using TESAmerica.Infrastructure.Presistence;

namespace TESAmerica.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbset;
        protected TESAmericaContext _db;
        public GenericRepository(TESAmericaContext context)
        {
            _db = context;
            _dbset = context.Set<T>();
        }
        public async Task Add(T entity)
        {
            await _dbset.AddAsync(entity);
        }
        public async Task<T> AddAndReturn(T entity)
        {
            var add = await _dbset.AddAsync(entity);
            return add.Entity;
        }
        public async Task AddRange(List<T> entities)
        {
            await _dbset.AddRangeAsync(entities);
        }
        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }
        public void DeleteRange(List<T> entities)
        {
            _dbset.RemoveRange(entities);
        }
        public void Update(T entity)
        {
            _dbset.Update(entity);
        }
        public async Task<T> Find(object id)
        {
            return await _dbset.FindAsync(id);
        }
        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            return await _dbset.Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
        public async Task<T> FindFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await _dbset.FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByQuery(string query)
        {
            return await Task.FromResult(_dbset.FromSqlRaw(query));
        }

        public async Task<T> FindFirstOrDefaultByQuery(string query)
        {
            return await Task.FromResult(_dbset.FromSqlRaw(query).FirstOrDefault());
        }

        public async Task<string> Max(Expression<Func<T, string>> predicate)
        {
            return await _dbset.MaxAsync(predicate);
        }
    }
}
