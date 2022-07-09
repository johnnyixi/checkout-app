using CheckoutApp.DataAccess.Exceptions;
using CheckoutApp.DataAccess.Interfaces;

namespace CheckoutApp.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : class, new()
{
    protected readonly CheckoutContext context;

    public Repository(CheckoutContext context)
    {
        this.context = context;
    }

    public async Task AddAsync(T entity)
    {
        if (entity == null)
        {
            throw new EntityNullRepositoryException();
        }

        try
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        } catch (Exception ex)
        {
            throw new AddAsyncRepositoryException(ex);
        }
    }

    public async Task<T> GetAsync(Guid id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new EntityNullRepositoryException();
        }

        try
        {
            context.Update(entity);
            await context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new UpdateAsyncRepositoryException(ex);
        }
    }
}
