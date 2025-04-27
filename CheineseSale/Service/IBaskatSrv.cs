using CheineseSale.Models;

namespace CheineseSale.Service
{
    public interface IBaskatSrv
    {
        public List<GiftsWithDonor> GetBasket();
        public string AddToCart(int id);
        public string DeleteGiftFromBasket(int id);
    }
}
