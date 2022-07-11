using CheckoutApp.Business.Models;
using FluentValidation;

namespace CheckoutApp.Business.Validators;

public class CreateBasketRequestValidator : AbstractValidator<CreateBasketRequest>
{
    private const int NameMinimumLength = 5;
    private const string NameRegularExpression = "^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$";
    public CreateBasketRequestValidator()
    {
        RuleFor(x => x.Customer)
            .MinimumLength(NameMinimumLength)
            .WithMessage($"Property must have a length of at least {NameMinimumLength} characters.")
            .Matches(NameRegularExpression)
            .WithMessage("Property must be a valid customer name.");
    }
}
