using System;
using System.Collections.Generic;

namespace RapidPay.Models.Database.Entities;

public partial class User
{
    public long Id { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}
