namespace RapidPay.Domain.Services;
/// <summary>
/// Service to calculate the Payments Fee based on certain conditions
/// </summary>
public interface IPaymentFeesModule
{
    /// <summary>
    /// Gets the current Fee for the Payments, it's a number between 0 and 1
    /// </summary>
    /// <returns>The Payment Fee</returns>
    decimal GetFee();
}
