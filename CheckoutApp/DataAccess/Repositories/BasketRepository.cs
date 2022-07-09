using CheckoutApp.DataAccess.Interfaces;
using CheckoutApp.DataAccess.Models;

namespace CheckoutApp.DataAccess.Repositories;

public class BasketRepository : Repository<Basket>, IBasketRepository
{
    public BasketRepository(CheckoutContext context) : base(context) { }
}
