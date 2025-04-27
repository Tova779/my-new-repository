using System;
using System.Collections.Generic;

namespace CheineseSale.Models;

public partial class Gift
{
    public int GiftId { get; set; }

    public string GiftName { get; set; } = null!;

    public int CatagoryId { get; set; }

    public int Price { get; set; }

    public int DonterId { get; set; }

    public int? ImageId { get; set; }

    public string? Description { get; set; }

    public int Purchaser { get; set; }

    public virtual ICollection<Baskat> Baskats { get; set; } = new List<Baskat>();

    public virtual Catagory Catagory { get; set; } = null!;

    public virtual Donter Donter { get; set; } = null!;

    public virtual GiftsImage? Image { get; set; }

    public virtual ICollection<UserGift> UserGifts { get; set; } = new List<UserGift>();

    public virtual ICollection<Winner> Winners { get; set; } = new List<Winner>();
}
