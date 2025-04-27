using CheineseSale.Dal;
using CheineseSale.Models;

namespace CheineseSale.Service
{
    public class BaskatSrv : IBaskatSrv
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBaskat _Baskat;

        public BaskatSrv(IBaskat Baskat, IHttpContextAccessor httpContextAccessor)
        {
            _Baskat = Baskat;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<GiftsWithDonor> GetBasket()
        {
            try
            {
                var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("User token is missing.");
                }

                return _Baskat.GetBasket(token);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה, לדוגמה: החזרת שגיאה מותאמת אישית
                throw new Exception("An error occurred while retrieving the user's basket.", ex);
            }
        }

        public string AddToCart(int id)
        {
            try
            {
                var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("User token is missing.");
                }

                return _Baskat.AddToCart(id, token);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה, לדוגמה: החזרת שגיאה מותאמת אישית
                throw new Exception($"An error occurred while adding gift {id} to the cart.", ex);
            }
        }

        public string DeleteGiftFromBasket(int id)
        {
            try
            {
                var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("User token is missing.");
                }

                return _Baskat.DeleteGiftFromBasket(id, token);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה, לדוגמה: החזרת שגיאה מותאמת אישית
                throw new Exception($"An error occurred while deleting gift {id} from the basket.", ex);
            }
        }
    }
}
