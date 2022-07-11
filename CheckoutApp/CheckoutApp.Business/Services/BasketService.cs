using System.Text.RegularExpressions;
using AutoMapper;
using CheckoutApp.Business.Exceptions;
using CheckoutApp.Business.Models;
using CheckoutApp.DataAccess.Interfaces;
using CheckoutApp.DataAccess.Models;

using static CheckoutApp.Business.Constants.BasketServiceValidationConstants;

namespace CheckoutApp.Business.Services;

public class BasketService : IBasketService
{
    private readonly IBasketRepository _basketRepository;
    private readonly IVatService _vatService;
    private readonly IMapper _mapper;

    public BasketService(IBasketRepository basketRepository, IVatService vatService, IMapper mapper)
    {
        _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        _vatService = vatService ?? throw new ArgumentNullException(nameof(vatService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Guid> AddBasketAsync(string customer, bool paysVAT)
    {
        if (IsInvalidCustomer(customer))
        {
            throw new InvalidCustomerNameException();
        }

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

        basketResponse.TotalNet = GetTotalNetPrice(basketResponse.Items);
        basketResponse.TotalGross = GetTotalGrossPrice(basketResponse.TotalNet, basketResponse.PaysVAT);

        return basketResponse;
    }

    public async Task<CreateArticleLineResponse?> AddArticleLineToBasketAsync(Guid basketId, string itemName, decimal itemPrice)
    {
        var basket = await _basketRepository.GetBasketAsync(basketId);

        if (basket == null)
        {
            return null;
        }

        var articleLine = new ArticleLine
        {
            Item = itemName,
            Price = itemPrice
        };

        basket.Items.Add(articleLine);

        await _basketRepository.UpdateAsync(basket);

        var articleLineResponse = _mapper.Map<ArticleLine, CreateArticleLineResponse>(articleLine);

        return articleLineResponse;
    }

    public async Task<PayBasketResponse?> PayBasket(Guid basketId)
    {
        var basket = await _basketRepository.GetAsync(basketId);

        if (basket == null)
        {
            return null;
        }

        if (basket.Payed)
        {
            throw new BasketAlreadyPayedException(basket.Id);
        }

        basket.Closed = true;
        basket.Payed = true;

        var basketResponse = await _basketRepository.UpdateAsync(basket);

        return new PayBasketResponse
        {
            BasketId = basketResponse.Id,
            Closed = true,
            Payed = true
        };
    }

    private static decimal GetTotalNetPrice(IEnumerable<ArticleLineResponse> items)
        => items.Sum(item => item.Price);

    private static bool IsInvalidCustomer(string? customer)
        => customer is null 
           || customer.Length < CustomerMinimumLength 
           || !Regex.IsMatch(customer, CustomerRegularExpression);

    private decimal GetTotalGrossPrice(decimal totalNetPrice, bool paysVAT)
        => totalNetPrice * (paysVAT ? _vatService.GetDefaultVatRate() : 1.0m);
}
