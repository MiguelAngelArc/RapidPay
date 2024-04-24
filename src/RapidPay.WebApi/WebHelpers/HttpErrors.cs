using RapidPay.Models.Enums;

namespace RapidPay.WebApi.WebHelpers;

public static class HttpErrors {
    public static readonly IDictionary<string, HttpErrorInfo> CommonErrors = new Dictionary<string, HttpErrorInfo> {
        [ErrorCodes.UnknownError] = new HttpErrorInfo {
            HttpStatusCode = 500,
            ErrorCode = ErrorCodes.UnknownError
        },
        [ErrorCodes.CardNotFound] = new HttpErrorInfo {
            HttpStatusCode = 404,
            ErrorCode = ErrorCodes.CardNotFound
        },
        [ErrorCodes.CardNumberAlreadyExists] = new HttpErrorInfo {
            HttpStatusCode = 400,
            ErrorCode = ErrorCodes.CardNumberAlreadyExists
        },
        [ErrorCodes.MoneyInCardNotEnough] = new HttpErrorInfo {
            HttpStatusCode = 400,
            ErrorCode = ErrorCodes.MoneyInCardNotEnough
        },
        [ErrorCodes.UserNotFound] = new HttpErrorInfo {
            HttpStatusCode = 404,
            ErrorCode = ErrorCodes.UserNotFound
        },
        [ErrorCodes.EmailAlreadyInUse] = new HttpErrorInfo {
            HttpStatusCode = 400,
            ErrorCode = ErrorCodes.EmailAlreadyInUse
        },
        [ErrorCodes.WrongPassword] = new HttpErrorInfo {
            HttpStatusCode = 400,
            ErrorCode = ErrorCodes.WrongPassword
        }
    };
}
