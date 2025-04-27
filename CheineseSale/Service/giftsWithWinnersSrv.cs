using CheineseSale.Dal;
using CheineseSale.Models;

namespace CheineseSale.Service
{
    public class giftsWithWinnersSrv:IgiftsWithWinnersSrv
    {
        private readonly IgiftsWithWinnersDal _giftsWithWinnersDal;
        public giftsWithWinnersSrv(IgiftsWithWinnersDal iftsWithWinnersDal)
        {
            _giftsWithWinnersDal = iftsWithWinnersDal;
        }
        public List<giftsWithWinners> GetGiftWitWinners()
        {
            return _giftsWithWinnersDal.GetGiftWitWinners();
        }
    }
}
