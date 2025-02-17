using Microsoft.AspNetCore.Mvc;
using RapidPay.Models.Enums;
using RapidPay.WebApi.WebHelpers;

namespace RapidPay.WebApi.Controllers;
/// <summary>
/// Base controller for all the application controllers with a default method to handle app errors
/// </summary>
public class BaseController : ControllerBase {
    private readonly ILogger<BaseController> _logger;

    public BaseController(ILogger<BaseController> logger)
    {
        _logger = logger;
    }
    /// <summary>
    /// Handles an exception and perform some logic to return a custom error code or unknown error in case needed,
    //  this way we have homogeneous error structure
    /// </summary>
    /// <param name="e">The exception catched in a try block</param>
    /// <returns>An object result with the information about the error with default structure</returns>
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