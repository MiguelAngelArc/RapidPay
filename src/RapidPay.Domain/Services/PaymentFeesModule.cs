namespace RapidPay.Domain.Services;

public class PaymentFeesModule : IPaymentFeesModule
{
    public decimal GetFee()
    {
       var rawFee = new Random().NextDouble() * 2;
       return Math.Round(Convert.ToDecimal(rawFee), 4);
    }
}
