using CheckoutApp.DataAccess.Exceptions;
using CheckoutApp.DataAccess.Interfaces;

namespace CheckoutApp.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : class, new()
{
    protected readonly CheckoutContext Context;

    public Repository(CheckoutContext context)
    {
        Context = context;
    }

    public async Task AddAsync(T entity)
    {
        if (entity == null)
        {
            throw new EntityNullRepositoryException();
        }

        try
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new AddAsyncRepositoryException(ex);
        }
    }

    public async Task<T?> GetAsync(Guid id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new EntityNullRepositoryException();
        }

        try
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new UpdateAsyncRepositoryException(ex);
        }
    }
}
