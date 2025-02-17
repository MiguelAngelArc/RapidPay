using DbModels = RapidPay.Models.Database.Entities;
using DTOs = RapidPay.Models.DTOs;

namespace RapidPay.Domain.Services;
/// <summary>
/// Service to manage the cards of the users
/// </summary>
public interface ICardManagementService
{
    /// <summary>
    /// Creates a card for the user
    /// </summary>
    /// <param name="card">The card object containing the relevant info like the user id and card number</param>
    /// <returns>The created card for the user</returns>
    Task<DTOs.Card> CreateCard(DTOs.Card card);
    /// <summary>
    /// Performs a Payment for the user
    /// </summary>
    /// <param name="payment">The Payment object containing the relevant information</param>
    /// <returns>A flag indicating whether the Payment was successful</returns>
    Task<bool> Pay(DTOs.Payment payment);
    /// <summary>
    /// Gets the Balance of one of the cards of the user
    /// </summary>
    /// <param name="cardId">The card identifier</param>
    /// <param name="userId">The user identifier</param>
    /// <returns>A card object containg all the info about the card including the balance</returns>
    Task<DTOs.Card> GetCardBalance(long cardId, long userId);
}
