using System.Security.Claims;
using RapidPay.Models.DTOs;

namespace RapidPay.Domain.Services;

public interface IJwtManager {
    JsonWebToken GenerateToken(JwtUser user);
    (ClaimsPrincipal claims, JwtUser jwtUser) ReadToken(string token, bool validateLifeTime);
    int TokenTTLSeconds { get; }
    int RefreshTokenTTLSeconds { get; }
}
