using CheckoutApp.Business.Models;
using FluentValidation;
using static CheckoutApp.Business.Constants.BasketServiceValidationConstants;

namespace CheckoutApp.Business.Validators;

public class CreateBasketRequestValidator : AbstractValidator<CreateBasketRequest>
{
    public CreateBasketRequestValidator()
    {
        RuleFor(x => x.Customer)
            .MinimumLength(CustomerMinimumLength)
            .WithMessage($"Property must have a length of at least {CustomerMinimumLength} characters.")
            .Matches(CustomerRegularExpression)
            .WithMessage("Property must be a valid customer name.");
    }
}
