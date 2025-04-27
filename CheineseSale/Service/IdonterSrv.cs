using CheineseSale.Models;

namespace CheineseSale.Service
{
    public interface IdonterSrv
    {
        public void add(Donter donter);
        public List<Donter> GetDonters();
        public List<Gift> GetDonorGifts(int id);
        public void DeleteDonter(int id);
        public void Update(Donter donter);
        public List<Donter> GetDontersByName(string DonterFirstName);
        public List<Donter> GetDontersByEmail(string DonterMail);
        public List<Donter> GetDontersByGiftName(string giftName);
        public Donter? GetDonterById(int donterId);
    }

}

