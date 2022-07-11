using CheckoutApp.DataAccess.Interfaces;
using CheckoutApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckoutApp.DataAccess.Repositories;

public class BasketRepository : Repository<Basket>, IBasketRepository
{
    public BasketRepository(CheckoutContext context) : base(context)
    {

    }

    public async Task<Basket?> GetBasketAsync(Guid basketId)
    {
        var firstOrDefault = await Context.Set<Basket>().Include(basket => basket.Items).FirstOrDefaultAsync(basket => basket.Id == basketId);

        return firstOrDefault;
    }
}
