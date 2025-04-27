using System;
using System.Collections.Generic;

namespace CheineseSale.Models;

public partial class GiftsImage
{
    public int ImageId { get; set; }

    public string ImageName { get; set; } = null!;

    public virtual ICollection<Gift> Gifts { get; set; } = new List<Gift>();
}
