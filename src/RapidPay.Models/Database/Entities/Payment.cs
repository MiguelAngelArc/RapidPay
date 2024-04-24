using System;
using System.Collections.Generic;

namespace RapidPay.Models.Database.Entities;

public partial class Payment
{
    public long Id { get; set; }

    public long CardId { get; set; }

    public decimal ItemPrice { get; set; }

    public decimal Fee { get; set; }

    public virtual Card Card { get; set; } = null!;
}
