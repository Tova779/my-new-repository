using CheineseSale.Models;

namespace CheineseSale.Dal
{
    public interface IUserGiftDal
    {
        List<User> GetPurchasesForGift(int giftId);
        public List<User> GetAllBuyers();
        public List<GiftsWithDonor> GetGiftsSortedByPrice();
        public List<GiftsWithDonor> GetGiftsSortedByPurchaser();
        public string BuyGift(int id, string token);
        public List<GiftsWithDonor> GetAllPurchase(string token);
    }
}
