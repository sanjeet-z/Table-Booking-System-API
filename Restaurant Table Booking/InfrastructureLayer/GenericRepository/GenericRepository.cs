using ApplicationLayer.IGeneric;
using InfrastructureLayer.Data;

namespace InfrastructureLayer.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AuthDbContext _authDbContext;
        public GenericRepository(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public async Task CreateAsync(T entity) => await _authDbContext.Set<T>().AddAsync(entity);

        public IQueryable<T> GetAllAsync()
        {
            var bookings = _authDbContext.Set<T>();
            return bookings;
        }
        public Task DeleteAsync(T existngData)
        {
            _authDbContext.Set<T>().Remove(existngData);
            return Task.CompletedTask;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _authDbContext.Set<T>().FindAsync(id);

        }

        public Task UpdateAsync(T entity)
        {
            _authDbContext.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

       /* public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification = null)
        {
            return ApplySpecificationForList(specification);
        }*/

       /* public async Task<T> FindAsync(ISpecification<T> specification = null)
        {
            return await ApplySpecificationForList(specification).FirstOrDefaultAsync();
        }*/

/*        private IQueryable<T> ApplySpecificationForList(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(_authDbContext.Set<T>().AsQueryable(), specification);
        }*/
    }
}
