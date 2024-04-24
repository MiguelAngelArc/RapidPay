using Microsoft.EntityFrameworkCore;
using RapidPay.Models.Database;
using RapidPay.Models.Enums;
using DbModels = RapidPay.Models.Database.Entities;
using DTOs = RapidPay.Models.DTOs;

namespace RapidPay.Domain.Services;

public class CardManagementService : ICardManagementService
{
    private readonly IPaymentFeesModule _paymentFeesModule;
    private readonly RapidPayDBContext _unitOfWork;

    public CardManagementService(IPaymentFeesModule paymentFeesModule, RapidPayDBContext unitOfWork)
    {
        _paymentFeesModule = paymentFeesModule;
        _unitOfWork = unitOfWork;
    }

    public async Task<DTOs.Card> CreateCard(DTOs.Card card)
    {
        var oldCard = await _unitOfWork.Cards.FirstOrDefaultAsync(c => c.Number == card.Number);
        
        if (oldCard != null)
            throw new Exception(ErrorCodes.CardNumberAlreadyExists);

        var dbCard = new DbModels.Card {
            Number = card.Number!,
            Balance = Math.Round(card.Balance, 4),
            UserId = card.UserId!.Value
        };
        await _unitOfWork.Cards.AddAsync(dbCard);
        await _unitOfWork.SaveChangesAsync();
        card.Id = dbCard.Id;
        return card;
    }

    public async Task<DTOs.Card> GetCardBalance(long cardId, long userId)
    {
        var card = await _unitOfWork.Cards.FirstOrDefaultAsync(c => c.Id == cardId)
            ?? throw new Exception(ErrorCodes.CardNotFound);

        if (card.UserId != userId)
            throw new Exception(ErrorCodes.CardDoesNotBelongToUser);

        return new DTOs.Card {
            Id = card.Id,
            UserId = card.UserId,
            Balance = card.Balance,
            Number = card.Number
        };
    }

    public async Task<bool> Pay(DTOs.Payment payment)
    {
        var card = await _unitOfWork.Cards.FirstOrDefaultAsync(c => c.Id == payment.CardId)
            ?? throw new Exception(ErrorCodes.CardNotFound);
        
        if (card.UserId != payment.UserId)
            throw new Exception(ErrorCodes.CardDoesNotBelongToUser);

        var fee = _paymentFeesModule.GetFee();
        var finalItemPrice = Math.Round(payment.ItemPrice * (1 + fee), 4);

        if (card.Balance < finalItemPrice)
            throw new Exception(ErrorCodes.MoneyInCardNotEnough);

        var dbPayment = new DbModels.Payment {
            Fee = fee,
            ItemPrice = payment.ItemPrice,
            CardId = payment.CardId
        };

        card.Balance -= finalItemPrice;
        await _unitOfWork.Payments.AddAsync(dbPayment);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
