using CheineseSale.Dal;
using CheineseSale.Models;

namespace CheineseSale.Service
{
    public interface IRaffleSrv
    {
        public User GetWinner(int id);
        public void SaveWinnersToExcel();
        public void SaveTotalMoneyToExcel();
        public List<WinnersWithGifts> getAllWinners();

    }
}
