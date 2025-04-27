using CheineseSale.Dal;
using CheineseSale.Models;

namespace CheineseSale.Service
{
    public class CategorySrv : ICategorySrv
    {
        private readonly ICategoryDal _CategoryDal;

        public CategorySrv(ICategoryDal CategoryDal)
        {
            _CategoryDal = CategoryDal;
        }

        public List<Catagory> GetAll()
        {
            try
            {
                return _CategoryDal.GetAll();
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה, לדוגמה: החזרת שגיאה מותאמת אישית
                throw new Exception("An error occurred while retrieving the categories.", ex);
            }
        }
    }
}
