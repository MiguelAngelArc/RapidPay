using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Domain.Services;
using RapidPay.WebApi.WebHelpers;

namespace RapidPay.WebApi.Controllers;

using DTOs = RapidPay.Models.DTOs;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CardsController : BaseController
{
    private readonly ILogger<CardsController> _logger;
    private readonly ICardManagementService _cardManagementService;
    private readonly IHttpContextAccessor _contextAccesor;

    public CardsController(
        ILogger<CardsController> logger, ICardManagementService cardManagementService, IHttpContextAccessor contextAccesor
    ) : base(logger)
    {
        _logger = logger;
        _cardManagementService = cardManagementService;
        _contextAccesor = contextAccesor;
    }

    [HttpPost(Name = "CreateCard")]
    public async Task<IActionResult> Post([FromBody] DTOs.Card card)
    {
        try {
            card.UserId = _contextAccesor.GetUserIdentity().UserId;
            var result = await _cardManagementService.CreateCard(card);
            return Ok(result);
        }
        catch (Exception e){
            return DefaultCatch(e);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Post([FromRoute] long id)
    {
        try {
            var userId = _contextAccesor.GetUserIdentity().UserId;
            var result = await _cardManagementService.GetCardBalance(id, userId);
            return Ok(result);
        }
        catch (Exception e){
            return DefaultCatch(e);
        }
    }

    [HttpPost("pay")]
    public async Task<IActionResult> Payment([FromBody] DTOs.Payment payment)
    {
        try {
            payment.UserId = _contextAccesor.GetUserIdentity().UserId;
            var result = await _cardManagementService.Pay(payment);
            return Ok(new {
                successful = result
            });
        }
        catch (Exception e){
            return DefaultCatch(e);
        }
    }
}
