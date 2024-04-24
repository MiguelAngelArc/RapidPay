using System.ComponentModel.DataAnnotations;
using RapidPay.Models.Enums;

namespace RapidPay.Models.DTOs;

public class SignUpModel : SignInModel {
    [Required(ErrorMessage = ErrorCodes.InvalidUserName)]
    [MinLength(4, ErrorMessage = ErrorCodes.InvalidUserName)]
    public string UserName { get; set; }
}
