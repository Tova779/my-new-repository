using CheineseSale.Dal;
using CheineseSale.Models;

namespace CheineseSale.Service
{
    public class RaffleSrv : IRaffleSrv
    {
        private readonly IRaffleDal _raffleDal;
        private readonly ILogger<RaffleSrv> _logger;

        public RaffleSrv(IRaffleDal raffleDal, ILogger<RaffleSrv> logger)
        {
            _raffleDal = raffleDal;
            _logger = logger;
        }
        public User GetWinner(int id)
        {
            try
            {
                var winner = _raffleDal.GetWinner(id);  // שמור את המנצח במשתנה
                _logger.LogInformation("{id} Winner", id);  // כעת הלוג ייכתב
                return winner;  // מחזיר את המנצח
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה, לדוגמה: לוג או החזרת שגיאה מותאמת אישית
                throw new Exception($"An error occurred while retrieving the winner for ID {id}.", ex);
            }
        }


        public void SaveWinnersToExcel()
        {
            try
            {
                _raffleDal.SaveWinnersToExcel();
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה, לדוגמה: לוג או החזרת שגיאה מותאמת אישית
                throw new Exception("An error occurred while saving winners to Excel.", ex);
            }
        }

        public void SaveTotalMoneyToExcel()
        {
            try
            {
                _raffleDal.SaveTotalMoneyToExcel();
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה, לדוגמה: לוג או החזרת שגיאה מותאמת אישית
                throw new Exception("An error occurred while saving total money to Excel.", ex);
            }
        }

        public List<WinnersWithGifts> getAllWinners()
        {
            try
            {
                return _raffleDal.getAllWinners();
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה, לדוגמה: לוג או החזרת שגיאה מותאמת אישית
                throw new Exception("An error occurred while retrieving all winners.", ex);
            }
        }
    }
}
