using CheineseSale.Dal;
using CheineseSale.Models;
using Microsoft.Extensions.Logging;
using System.Drawing;

namespace CheineseSale.Service
{
    public class giftService : IgiftSrvice
    {
        private readonly IgiftDal _giftDal;
        private readonly ILogger<giftService> _logger;

        public giftService(IgiftDal giftDal, ILogger<giftService> logger)
        {
            _giftDal = giftDal;
            _logger = logger;
        }

        public IEnumerable<GiftsWithDonor> Get()
        {
            try
            {
                return _giftDal.GetGifts();
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error in Get method: {ex.Message}", ex);
                throw new Exception("An error occurred while retrieving gifts.", ex); // מרים את השגיאה מחדש
            }
        }

        public void Add(GiftsWithDonor gift)
        {
            try
            {
                _giftDal.add(gift);
                _logger.LogInformation("gift 👉 {gift.GiftName} created successfully", gift.GiftName);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Add method: {ex.Message}", ex);
                throw new Exception("An error occurred while adding a gift.", ex);
            }
        }

        public void Update(GiftsWithDonor gift)
        {
            try
            {
                _giftDal.Update(gift);
                _logger.LogInformation("gift 👉 {gift.GiftName} Update successfully", gift.GiftName);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error in Update method: {ex.Message}", ex);
                throw new Exception("An error occurred while updating the gift.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                _giftDal.Delete(id);


            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error in Delete method: {ex.Message}", ex);
                throw new Exception("An error occurred while deleting the gift.", ex);
            }
        }

        public List<GiftsWithDonor> GetGiftsByName(string giftName)
        {
            try
            {
                return _giftDal.GetGiftsByName(giftName).ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error in GetGiftsByName method: {ex.Message}", ex);
                throw new Exception("An error occurred while retrieving gifts by name.", ex);
            }
        }

        public List<GiftsWithDonor> GetGiftsByDonterName(string donterFirstName)
        {
            try
            {
                return _giftDal.GetGiftsByDonterName(donterFirstName);
            }
            catch (Exception ex)
            { 
            //    _logger.LogError($"Error in GetGiftsByDonterName method: {ex.Message}", ex);
                throw new Exception("An error occurred while retrieving gifts by donor name.", ex);
            }
        }

        public List<GiftsWithDonor> SearchGiftsByPurchasers(int minPurchasers)
        {
            try
            {
                return _giftDal.SearchGiftsByPurchasers(minPurchasers);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error in SearchGiftsByPurchasers method: {ex.Message}", ex);
                throw new Exception("An error occurred while searching gifts by purchasers.", ex);
            }
        }

        public GiftsWithDonor GetGiftsById(int id)
        {
            try
            {
                return _giftDal.GetGiftsById(id);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error in GetGiftsById method: {ex.Message}", ex);
                throw new Exception("An error occurred while retrieving a gift by ID.", ex);
            }
        }
    }
}
