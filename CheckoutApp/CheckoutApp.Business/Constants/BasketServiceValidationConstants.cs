namespace CheckoutApp.Business.Constants;

public static class BasketServiceValidationConstants
{
    public const int CustomerMinimumLength = 5;
    public const string CustomerRegularExpression = "^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$";
}
