namespace CheckoutApp.DataAccess.Interfaces;

internal interface IRepository<T> where T : class, new()
{
    Task AddAsync(T entity);
    Task<T> GetAsync(Guid id);
    Task<T> UpdateAsync(T entity);
}
