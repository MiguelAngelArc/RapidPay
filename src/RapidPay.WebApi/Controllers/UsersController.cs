namespace RapidPay.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using RapidPay.Domain.Services;
using RapidPay.Models.DTOs;

/// <summary>
/// User Login controller
/// </summary>
[ApiController]
[Route("[controller]")]
public class UsersController : BaseController {
    private readonly ILogger<UsersController> _logger;

    private readonly IAuthService _usersDomain;

    public UsersController(IAuthService usersDomain, ILogger<UsersController> logger) : base(logger)
    {
        _usersDomain = usersDomain;
        _logger = logger;
    }

    /// <summary>
    /// Endpoint to sign in user
    /// </summary>
    /// <param name="signInModel">The sign in payload</param>
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel) {
        try{
            var token = await _usersDomain.SignIn(signInModel);
            return Ok(token);
        }
        catch (Exception e) {
            return DefaultCatch(e);
        }
    }

    /// <summary>
    /// Endpoint to sign up user
    /// </summary>
    /// <param name="signUpModel">The sign up payload</param>
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel) {
        try {
            var token = await _usersDomain.SignUp(signUpModel);
            return Ok(token);
        }
        catch (Exception e) {
            return DefaultCatch(e);
        }
    }
}
