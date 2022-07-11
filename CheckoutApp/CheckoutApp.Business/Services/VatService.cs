namespace CheckoutApp.Business.Services;

public class VatService : IVatService
{
    private const decimal DefaultVatRate = 1.1m;
    public decimal GetDefaultVatRate() => DefaultVatRate;
}
