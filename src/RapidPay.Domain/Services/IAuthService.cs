using RapidPay.Models.DTOs;

namespace RapidPay.Domain.Services;

public interface IAuthService {
    Task<JsonWebToken> SignIn(SignInModel signInModel);
    Task<JsonWebToken> SignUp(SignUpModel signUpModel);
}
