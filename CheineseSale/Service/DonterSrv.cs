using CheineseSale.Dal;
using CheineseSale.Models;

namespace CheineseSale.Service
{
    public class DonterSrv: IdonterSrv
    {
        private readonly IDonterDal _DonterDal;
        private readonly ILogger<DonterSrv> _logger;
        public DonterSrv(IDonterDal DonterDal, ILogger<DonterSrv> logger)
        {
            _DonterDal = DonterDal;
            _logger = logger;
        }
        public void add(Donter donter)
        {
            try
            {
                _DonterDal.add(donter);
                _logger.LogInformation("donter 👉 {donter.DonterFirstName} add successfully", donter.DonterFirstName);
            }
            catch (Exception)
            {
                throw;  // העברת השגיאה הלאה
            }
        }

        public List<Donter> GetDonters()
        {
            try
            {
                return _DonterDal.GetDonters();
            }
            catch (Exception)
            {
                throw;  // העברת השגיאה הלאה
            }
        }

        public List<Gift> GetDonorGifts(int id)
        {
            try
            {
                return _DonterDal.GetDonorGifts(id);
            }
            catch (Exception)
            {
                throw;  // העברת השגיאה הלאה
            }
        }

        public void DeleteDonter(int id)
        {
            try
            {
                _DonterDal.DeleteDonter(id);
                _logger.LogInformation("donter 👉 {id} Delet successfully", id);
            }
            catch (Exception)
            {
                throw;  // העברת השגיאה הלאה
            }
        }

        public void Update(Donter donter)
        {
            try
            {
                _DonterDal.Update(donter);
                _logger.LogInformation("donter 👉 {donter.DonterFirstName} Update successfully", donter.DonterFirstName);
            }
            catch (Exception)
            {
                throw;  // העברת השגיאה הלאה
            }
        }

        public List<Donter> GetDontersByName(string DonterFirstName)
        {
            try
            {
                return _DonterDal.GetDontersByName(DonterFirstName);
            }
            catch (Exception)
            {
                throw;  // העברת השגיאה הלאה
            }
        }

        public List<Donter> GetDontersByEmail(string DonterMail)
        {
            try
            {
                return _DonterDal.GetDontersByEmail(DonterMail);
            }
            catch (Exception)
            {
                throw;  // העברת השגיאה הלאה
            }
        }

        public List<Donter> GetDontersByGiftName(string giftName)
        {
            try
            {
                return _DonterDal.GetDontersByGiftName(giftName);
            }
            catch (Exception)
            {
                throw;  // העברת השגיאה הלאה
            }
        }

        public Donter? GetDonterById(int donterId)
        {
            try
            {
                return _DonterDal.GetDonterById(donterId);
            }
            catch (Exception)
            {
                throw;  // העברת השגיאה הלאה
            }
        }
    }
}