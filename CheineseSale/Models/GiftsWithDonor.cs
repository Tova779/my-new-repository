namespace CheineseSale.Models
{
    public class GiftsWithDonor
    {
     
        public int GiftId { get; set; }

        public string GiftName { get; set; } = null!;

        public string? Description { get; set; }

        public string DonorName { get; set; } = null!;

        public int Price { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? ImageGift { get; set; }
        public int Purchaser { get; set; }
    }


}

