using CheineseSale.Models;

namespace CheineseSale.Dal
{
    public interface IgiftDal
    {
        //public IEnumerable<Gift> Get();
        public void add(GiftsWithDonor gift);
        public void Update(GiftsWithDonor gift);
        public void Delete(int id);
        public List<GiftsWithDonor> GetGifts();
        //public GiftsWithDonor GetGiftsByName(string giftName);
        public List<GiftsWithDonor> GetGiftsByName(string giftName);
        public List<GiftsWithDonor> GetGiftsByDonterName(string donterFirstName);
        public List<GiftsWithDonor> SearchGiftsByPurchasers(int minPurchasers);
        public GiftsWithDonor GetGiftsById(int id);
    }
}
