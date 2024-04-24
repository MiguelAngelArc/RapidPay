using System.ComponentModel.DataAnnotations;

namespace RapidPay.Models.DTOs;

public class Card
{   
    public long? Id { get; set; }
    public long? UserId { get; set; }
    [Required]
    [RegularExpression("^[0-9]{15}$", ErrorMessage = "Value not allowed for CardNumber")]
    public string Number { get; set; }
    public decimal Balance { get; set; }
}
