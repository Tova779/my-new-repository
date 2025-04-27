namespace CheineseSale.Models
{
    public class WinnersWithGifts
    {
        
        public string Name { get; set; } = null!;
        public string? UserEmail { get; set; }
        public string UserPhone { get; set; } = null!;
        public string UserAdress { get; set; } = null!;
        public string GiftName { get; set; } = null!;
        public int GiftId { get; set; }

    }
}
