using CheineseSale.Models;

namespace CheineseSale.Dal
{
    public interface IDonterDal
    {
        public void add(Donter donter);
        public void Update(Donter donter);
        public List<Donter> GetDonters();
        public List<Gift> GetDonorGifts(int id);
        public void DeleteDonter(int id);
        public List<Donter> GetDontersByName(string DonterFirstName);
        public List<Donter> GetDontersByEmail(string DonterMail);

        public List<Donter> GetDontersByGiftName(string giftName);
        public Donter? GetDonterById(int donterId);
    }
}
