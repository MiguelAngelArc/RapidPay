using Microsoft.AspNetCore.Mvc;
using RapidPay.Models.Enums;
using RapidPay.WebApi.WebHelpers;

namespace RapidPay.WebApi.Controllers;

public class BaseController : ControllerBase {
    private readonly ILogger<BaseController> _logger;

    public BaseController(ILogger<BaseController> logger)
    {
        _logger = logger;
    }

    protected ObjectResult DefaultCatch(Exception e) {
        var isAManagedError = HttpErrors.CommonErrors.TryGetValue(e.Message, out var errorInfo);
        if (!isAManagedError) {
            //We should never expose real exceptions, so we will catch all unknown exceptions 
            // (DatabaseErrors, Null Errors, Index errors, etc...) and rethrow an UnknownError after log
            _logger.LogInformation(e.Message);
            errorInfo = HttpErrors.CommonErrors[ErrorCodes.UnknownError];
        }
        return new ObjectResult(errorInfo) {
            StatusCode = errorInfo!.HttpStatusCode
        };

    }
}