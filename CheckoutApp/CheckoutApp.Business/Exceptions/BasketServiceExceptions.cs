namespace CheckoutApp.Business.Exceptions;

public class BasketServiceExceptions : Exception
{
    public BasketServiceExceptions()
    {
    }

    public BasketServiceExceptions(string message)
        : base(message)
    {
    }

    public BasketServiceExceptions(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class InvalidCustomerNameException : BasketServiceExceptions
{
    public InvalidCustomerNameException():base("The provided Customer value is not valid.")
    {
        
    }
}

public class BasketAlreadyPayedException : BasketServiceExceptions
{
    public BasketAlreadyPayedException(Guid basketId):base($"Basket with Id: [${basketId}] has already been payed.")
    {
        
    }
}
