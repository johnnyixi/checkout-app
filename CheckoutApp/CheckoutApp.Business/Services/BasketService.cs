using AutoMapper;
using CheckoutApp.Business.Models;
using CheckoutApp.DataAccess.Interfaces;
using CheckoutApp.DataAccess.Models;

namespace CheckoutApp.Business.Services;

public class BasketService : IBasketService
{
    private readonly IBasketRepository _basketRepository;
    private readonly IVatService _vatService;
    private readonly IMapper _mapper;

    public BasketService(IBasketRepository basketRepository, IVatService vatService, IMapper mapper)
    {
        _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        _vatService = vatService;
        _mapper = mapper;
    }

    public async Task<Guid> AddBasketAsync(string customer, bool paysVAT)
    {
        var basket = new Basket
        {
            Customer = customer,
            PaysVAT = paysVAT
        };

        await _basketRepository.AddAsync(basket);

        return basket.Id;
    }

    public async Task<BasketResponse?> GetBasketAsync(Guid basketId)
    {
        var basket = await _basketRepository.GetBasketAsync(basketId);

        if (basket is null)
        {
            return null;
        }

        var basketResponse = _mapper.Map<Basket, BasketResponse>(basket);

        basketResponse.TotalGross = basketResponse.TotalNet * (basketResponse.PaysVAT ? _vatService.GetDefaultVatRate() : 1.0m);

        return basketResponse;
    }

    public async Task<Basket?> AddArticleLineToBasketAsync(Guid basketId, string itemName, decimal itemPrice)
    {
        var basket = await _basketRepository.GetBasketAsync(basketId);

        if (basket == null)
        {
            throw new Exception("Basket not found!");
        }

        var articleLine = new ArticleLine
        {
            Item = itemName,
            Price = itemPrice
        };

        basket.Items.Add(articleLine);

        await _basketRepository.UpdateAsync(basket);

        return basket;
    }

    public async Task PayBasket(Guid basketId)
    {
        var basket = await _basketRepository.GetAsync(basketId);

        if (basket == null)
        {
            throw new Exception("Basket not found!");
        }

        basket.Closed = true;
        basket.Payed = true;

        await _basketRepository.UpdateAsync(basket);
    }
}
