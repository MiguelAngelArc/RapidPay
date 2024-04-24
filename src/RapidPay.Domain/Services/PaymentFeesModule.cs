namespace RapidPay.Domain.Services;

public class PaymentFeesModule : IPaymentFeesModule
{
    public double GetFee()
    {
       return new Random().NextDouble() * 2;
    }
}
