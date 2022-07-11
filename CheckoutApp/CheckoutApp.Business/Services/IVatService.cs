namespace CheckoutApp.Business.Services;

/// <summary>
/// A services that returns the VAT rate.
/// </summary>
public interface IVatService
{
    /// <summary>
    /// Returns the default VAT rate.
    /// </summary>
    /// <returns>VAT rate.</returns>
    decimal GetDefaultVatRate();
}
