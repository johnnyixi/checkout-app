using CheckoutApp.Business.Models;
using FluentValidation;

namespace CheckoutApp.Business.Validators;
public class CreateArticleLineRequestValidator : AbstractValidator<CreateArticleLineRequest>
{
    private const int ItemMinimumLength = 3;
    private const decimal ItemMinimumPrice = 0.00m;
    private const decimal ItemMaximumPrice = 999999.99m;
    private const int PriceScale = 2;
    private const int PricePrecision = 8;

    public CreateArticleLineRequestValidator()
    {
        RuleFor(createArticleRequest => createArticleRequest.Item)
            .MinimumLength(ItemMinimumLength)
            .WithMessage($"Property must have a length of at least {ItemMinimumLength} characters.");

        RuleFor(createArticleRequest => createArticleRequest.Price)
            .InclusiveBetween(ItemMinimumPrice, ItemMaximumPrice)
            .WithMessage($"Property must be a value between ${ItemMinimumPrice} and ${ItemMaximumPrice}")
            .ScalePrecision(PriceScale, PricePrecision)
            .WithMessage($"Property must have a valid decimal format (Decimals: {PriceScale}, Digits: {PricePrecision})");
    }
}
