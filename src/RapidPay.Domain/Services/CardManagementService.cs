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
            Balance = card.Balance,
            UserId = 1
        };
        await _unitOfWork.Cards.AddAsync(dbCard);
        await _unitOfWork.SaveChangesAsync();
        card.Id = dbCard.Id;
        return card;
    }

    public async Task<DTOs.Card> GetCardBalance(long cardId)
    {
        var card = await _unitOfWork.Cards.FirstOrDefaultAsync(c => c.Id == cardId)
            ?? throw new Exception(ErrorCodes.CardNotFound);
        return new DTOs.Card {
            Balance = card.Balance,
            Number = card.Number
        };
    }

    public async Task<bool> Pay(DTOs.Payment payment)
    {
        var card = await _unitOfWork.Cards.FirstOrDefaultAsync(c => c.Id == payment.CardId)
            ?? throw new Exception(ErrorCodes.CardNotFound);
        var fee = Convert.ToDecimal(_paymentFeesModule.GetFee());
        var finalItemPrice = payment.ItemPrice * (1 + fee);

        if (card.Balance < finalItemPrice)
            throw new Exception(ErrorCodes.MoneyInCardNotEnough);

        // var finalItemPrice = 
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
