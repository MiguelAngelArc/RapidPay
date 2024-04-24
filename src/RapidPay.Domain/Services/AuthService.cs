using Microsoft.EntityFrameworkCore;
using RapidPay.Models.Database;
using RapidPay.Models.DTOs;
using RapidPay.Models.Enums;

namespace RapidPay.Domain.Services;

using DbModels = RapidPay.Models.Database.Entities;

public class AuthService : IAuthService {
    private readonly RapidPayDBContext _unitOfWork;
    private readonly IJwtManager _jwtManager;

    public AuthService(RapidPayDBContext unitOfWork, IJwtManager jwtManager)
    {
        _unitOfWork = unitOfWork;
        _jwtManager = jwtManager;
    }

    public async Task<JsonWebToken> SignIn(SignInModel signInModel) {
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == signInModel.Email) 
            ?? throw new Exception(ErrorCodes.UserNotFound);

        if (user.Password != signInModel.Password)
            throw new Exception(ErrorCodes.WrongPassword);

        var userJwt = await CreateUserJwt(user.Id, user.Email, user.Name);
        return userJwt!;
    }

    public async Task<JsonWebToken> SignUp(SignUpModel signUpModel) {
        var userFound = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == signUpModel.Email);
        if (userFound != null) 
            throw new Exception(ErrorCodes.EmailAlreadyInUse);

        var newUser = new DbModels.User {
            Name = signUpModel.UserName,
            Email = signUpModel.Email,
            Password = signUpModel.Password // TODO: Use a password hasher (no enough time right now)
        };
        await _unitOfWork.Users.AddAsync(newUser);
        await _unitOfWork.SaveChangesAsync();
        var userJwt = await CreateUserJwt(newUser.Id, newUser.Email, newUser.Name);
        return userJwt;
    }

    private Task<JsonWebToken> CreateUserJwt(long userId, string email, string name) {
        var userJwt = _jwtManager.GenerateToken(new JwtUser {
            Name = name,
            Email = email,
            Id = userId
        });
        return Task.FromResult(userJwt);
    }
}
