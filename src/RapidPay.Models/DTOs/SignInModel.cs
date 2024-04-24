using System.ComponentModel.DataAnnotations;
using RapidPay.Models.Enums;

namespace RapidPay.Models.DTOs;

public class SignInModel {
    [Required(ErrorMessage = ErrorCodes.InvalidEmail)]
    [EmailAddress(ErrorMessage = ErrorCodes.InvalidEmail)]
    public string Email { get; set; }
    [Required(ErrorMessage = ErrorCodes.InvalidPassword)]
    [MinLength(6, ErrorMessage = ErrorCodes.InvalidPassword)]
    public string Password { get; set; }
}
