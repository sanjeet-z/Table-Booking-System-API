using Restaurant_Table_Booking.Application.ISpecification;

namespace ApplicationLayer.IGeneric
{
    public interface IGenericRepository<T> where T : class 
    {
        IQueryable<T> GetAllAsync();
        //Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification = null);
        //Task<T> FindAsync(ISpecification<T> specification = null);
        Task<T?> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T existngData);

    }
}
