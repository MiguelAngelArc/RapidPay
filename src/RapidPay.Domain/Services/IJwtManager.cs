using System.Security.Claims;
using RapidPay.Models.DTOs;

namespace RapidPay.Domain.Services;
/// <summary>
/// Service used to generate JWT tokens and also can read the value of the token
/// </summary>
public interface IJwtManager {
    /// <summary>
    /// Generates a JsonWebToken with the identity of the user
    /// </summary>
    /// <param name="user">The user identity</param>
    /// <returns>The generated JsonWebToken object including information about the user</returns>
    JsonWebToken GenerateToken(JwtUser user);
    /// <summary>
    /// Read a JsonWebToken as string and extracts the user identity and the claims of the token, there is a flag
    /// to indicate if we would like to validate the lifetime of the token (useful for refreshToken flow)
    /// </summary>
    /// <param name="token">The JWT token</param>
    /// <param name="validateLifeTime">Flag to indicate if we would like to validate the lifetime of the token</param>
    /// <returns>A tuple containing the claims of the token and the user identity</returns>
    (ClaimsPrincipal claims, JwtUser jwtUser) ReadToken(string token, bool validateLifeTime);
    /// <summary>
    /// Indicates the TTL of the generated tokens in seconds
    /// </summary>
    int TokenTTLSeconds { get; }
    /// <summary>
    /// Indicates the TTL of the generated refresh tokens in seconds
    /// </summary>
    int RefreshTokenTTLSeconds { get; }
}
