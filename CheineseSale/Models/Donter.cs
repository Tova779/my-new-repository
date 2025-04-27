using System;
using System.Collections.Generic;

namespace CheineseSale.Models;

public partial class Donter
{
    public int DonterId { get; set; }

    public string DonterFirstName { get; set; } = null!;

    public string DonterLastName { get; set; } = null!;

    public string DonterPhon { get; set; } = null!;

    public string? DonterMail { get; set; }

    public virtual ICollection<Gift> Gifts { get; set; } = new List<Gift>();
}
