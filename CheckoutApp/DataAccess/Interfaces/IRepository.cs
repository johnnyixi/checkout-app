namespace CheckoutApp.DataAccess.Interfaces;

public interface IRepository<T> where T : class, new()
{
    Task AddAsync(T entity);
    Task<T> GetAsync(Guid id);
    Task<T> UpdateAsync(T entity);
}
