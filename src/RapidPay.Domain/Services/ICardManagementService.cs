using DbModels = RapidPay.Models.Database.Entities;
using DTOs = RapidPay.Models.DTOs;

namespace RapidPay.Domain.Services;

public interface ICardManagementService
{
    Task<DTOs.Card> CreateCard(DTOs.Card card);
    Task<bool> Pay(DTOs.Payment payment);
    Task<DTOs.Card> GetCardBalance(long cardId, long userId);
}
