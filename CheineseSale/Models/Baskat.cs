using System;
using System.Collections.Generic;

namespace CheineseSale.Models;

public partial class Baskat
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int GiftId { get; set; }

    public int? Status { get; set; }

    public virtual Gift Gift { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

//public enum EnumBaskatStatus
//{
//    InCart=0, Purchass=1
//}