using System;
using System.Collections.Generic;

namespace CheineseSale.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Password { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? UserEmail { get; set; }

    public string UserAdress { get; set; } = null!;

    public string UserPhone { get; set; } = null!;

    public string UserRole { get; set; } = null!;

    public virtual ICollection<Baskat> Baskats { get; set; } = new List<Baskat>();

    public virtual ICollection<UserGift> UserGifts { get; set; } = new List<UserGift>();

    public virtual ICollection<Winner> Winners { get; set; } = new List<Winner>();
}
