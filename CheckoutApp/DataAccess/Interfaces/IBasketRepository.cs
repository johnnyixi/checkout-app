using CheckoutApp.DataAccess.Models;

namespace CheckoutApp.DataAccess.Interfaces;

public interface IBasketRepository : IRepository<Basket>
{
    Task<Basket?> GetBasketAsync(Guid basketId);
}
