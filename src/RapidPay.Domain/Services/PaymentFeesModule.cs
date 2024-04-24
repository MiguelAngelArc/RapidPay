namespace RapidPay.Domain.Services;

public class PaymentFeesModule : IPaymentFeesModule
{
    private decimal currentFee;
    private DateTime nextFeeDate;
    private const int numberOfSecondsBetweenEachFeeChange = 3600;

    public PaymentFeesModule() {
        currentFee = GenerateRandomeFee(1); // First fee should be less than 1 (< 100%) [I think]
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

    private decimal GenerateRandomeFee(int multiplier = 2) {
        var rawFee = new Random().NextDouble() * multiplier; // Generate number from 0 to 2
        return Math.Round(Convert.ToDecimal(rawFee), 4);
    }
}
