using RapidPay.Models.DTOs;

namespace RapidPay.Domain.Services;
/// <summary>
/// Service used to perform the common login operations, signIn and signUp 
/// </summary>
public interface IAuthService {
    /// <summary>
    /// Creates a JsonWebToken using the identity provided in the signInModel
    /// </summary>
    /// <param name="signInModel">The information about the user that is going to signIn</param>
    /// <returns>The JsonWebToken object including information about the user</returns>
    Task<JsonWebToken> SignIn(SignInModel signInModel);
    /// <summary>
    /// Creates a user in the Database and then creates a JsonWebToken using the identity provided in the signUpModel
    /// </summary>
    /// <param name="signInModel">The information about the user that is going to be signedUp</param>
    /// <returns>The JsonWebToken object including information about the user created</returns>
    Task<JsonWebToken> SignUp(SignUpModel signUpModel);
}
