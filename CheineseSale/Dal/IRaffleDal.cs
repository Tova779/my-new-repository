using CheineseSale.Models;

namespace CheineseSale.Dal
{
    public interface IRaffleDal
    {
        public User GetWinner(int id);
        public void SaveWinnersToExcel();
        public void SaveTotalMoneyToExcel();
        public List<WinnersWithGifts> getAllWinners();

    }
}
