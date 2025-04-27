using CheineseSale.Dal;
using CheineseSale.Models;

namespace CheineseSale.Service
{
    public class UserGiftSrv : IUserGiftSrv
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserGiftDal _userGiftDal;
        public UserGiftSrv(IUserGiftDal userGiftDal, IHttpContextAccessor httpContextAccessor)
        {
            _userGiftDal = userGiftDal;
            _httpContextAccessor = httpContextAccessor;
        }
        public List<User> GetPurchasesForGift(int giftId)
        {
            return _userGiftDal.GetPurchasesForGift(giftId);
        }
        public List<User> GetAllBuyers()
        {
            return _userGiftDal.GetAllBuyers();
        }
        public List<GiftsWithDonor> GetGiftsSortedByPrice()
        {
            return _userGiftDal.GetGiftsSortedByPrice();
        }
        //public List<Gift> GetGiftsSortedByPurchaseCount()
        public List<GiftsWithDonor> GetGiftsSortedByPurchaser()
        {
            return _userGiftDal.GetGiftsSortedByPurchaser();
        }
        public string BuyGift(int id)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("User token is missing.");
            }
            return _userGiftDal.BuyGift(id, token);
        }
        public List<GiftsWithDonor> GetAllPurchase()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("User token is missing.");
            }
            return _userGiftDal.GetAllPurchase(token);
        }
    }
}
