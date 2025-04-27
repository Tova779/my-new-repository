using CheineseSale.Models;

namespace CheineseSale.Service
{
    public interface IgiftSrvice
    {
        public IEnumerable<GiftsWithDonor> Get();
        public void Add(GiftsWithDonor gift);
        public void Update(GiftsWithDonor gift);
        public void Delete(int id);
        //public GiftsWithDonor GetGiftsByName(string giftName);
        public List<GiftsWithDonor> GetGiftsByName(string giftName);
        public List<GiftsWithDonor> GetGiftsByDonterName(string donterFirstName);
        public List<GiftsWithDonor> SearchGiftsByPurchasers(int minPurchasers);
        public GiftsWithDonor GetGiftsById(int id);
    }
}
