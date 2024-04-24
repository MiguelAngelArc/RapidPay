namespace RapidPay.Models.Enums;

public static class ErrorCodes
{
    public const string UnknownError = "unknown-error";
    public const string CardNotFound = "card-not-found";
    public const string CardNumberAlreadyExists = "card-number-already-exists";
    public const string MoneyInCardNotEnough = "money-in-card-not-enough";
    public const string UserNotFound = "user-not-found";
    public const string WrongPassword = "wrong-password";
    public const string InvalidUserName = "invalid-username";
    public const string InvalidEmail = "invalid-email";
    public const string InvalidPassword = "invalid-password";
    public const string EmailAlreadyInUse = "email-already-in-use";
}
