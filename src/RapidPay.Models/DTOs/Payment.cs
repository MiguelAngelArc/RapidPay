namespace RapidPay.Models.DTOs;

public class Payment
{
    public decimal ItemPrice { get; set; }
    public long CardId { get; set; }
    public long? UserId { get; set; }
}
