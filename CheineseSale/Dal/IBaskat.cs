using CheineseSale.Models;

namespace CheineseSale.Dal
{
    public interface IBaskat
    {
        public List<GiftsWithDonor> GetBasket(string token);
        public string AddToCart(int id, string token);
        public string DeleteGiftFromBasket(int id, string token);
    }
}
