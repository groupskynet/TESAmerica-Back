using System.Linq.Expressions;

namespace TESAmerica.Application.Contracts.Persistence
{
    public interface IGenericRepository<T>
    {
        Task Add(T entity);
        Task<T> AddAndReturn(T entity);
        Task AddRange(List<T> entities);
        void Delete(T entity);
        void DeleteRange(List<T> entities);
        void Update(T entity);
        Task<T> Find(object id);
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        public Task<T> FindFirstOrDefault(Expression<Func<T, bool>> predicate);
        public Task<IEnumerable<T>> GetAll();

        public Task<IEnumerable<T>> GetByQuery(string query);

        public Task<T> FindFirstOrDefaultByQuery(string query);
        public Task<string> Max(Expression<Func<T, string>> predicate);
    }
}
