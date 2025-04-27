using System;
using System.Collections.Generic;

namespace CheineseSale.Models;

public partial class Winner
{
    public int WinnerId { get; set; }

    public int GiftId { get; set; }

    public int UserId { get; set; }

    public virtual Gift Gift { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
