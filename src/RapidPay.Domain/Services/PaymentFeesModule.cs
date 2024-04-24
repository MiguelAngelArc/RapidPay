namespace RapidPay.Domain.Services;

public class PaymentFeesModule : IPaymentFeesModule
{
    private decimal currentFee;
    private DateTime nextFeeDate;
    private const int numberOfSecondsBetweenEachFeeChange = 3600;

    public PaymentFeesModule() {
        currentFee = GenerateRandomeFee();
        nextFeeDate = DateTime.Now.AddSeconds(numberOfSecondsBetweenEachFeeChange);
    }

    public decimal GetFee()
    {
        var now = DateTime.Now;
        if (now > nextFeeDate) {
            currentFee *= GenerateRandomeFee();
            nextFeeDate = now.AddSeconds(numberOfSecondsBetweenEachFeeChange);
        }
        return currentFee;
    }

    private decimal GenerateRandomeFee() {
        var rawFee = new Random().NextDouble() * 2; // Generate number from 0 to 2
        return Math.Round(Convert.ToDecimal(rawFee), 4);
    }
}
