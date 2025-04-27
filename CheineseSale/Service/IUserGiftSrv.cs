using CheineseSale.Models;

namespace CheineseSale.Service
{
    public interface IUserGiftSrv 
    {
        List<User> GetPurchasesForGift(int giftId);
        public List<User> GetAllBuyers();
        public List<GiftsWithDonor> GetGiftsSortedByPrice();
        public List<GiftsWithDonor> GetGiftsSortedByPurchaser();
        public string BuyGift(int id);
        public List<GiftsWithDonor> GetAllPurchase();
    }
}
