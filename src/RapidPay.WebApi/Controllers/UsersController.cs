namespace RapidPay.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using RapidPay.Domain.Services;
using RapidPay.Models.DTOs;

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

    // POST api/values
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel) {
        try{
            var token = await _usersDomain.SignIn(signInModel);
            // bool saveAuthInCookie = HttpContext.Request.Headers[AppConstants.SaveAuthInCookieHeader] == "true";
            return Ok(token);
        }
        catch (Exception e) {
            return DefaultCatch(e);
        }
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel) {
        try {
            var token = await _usersDomain.SignUp(signUpModel);
            // bool saveAuthInCookie = HttpContext.Request.Headers[AppConstants.SaveAuthInCookieHeader] == "true";
            return Ok(token);
        }
        catch (Exception e) {
            return DefaultCatch(e);
        }
    }
}
