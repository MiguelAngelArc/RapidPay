using System;
using System.Collections.Generic;

namespace RapidPay.Models.Database.Entities;

public partial class Card
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Number { get; set; } = null!;

    public decimal Balance { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}
