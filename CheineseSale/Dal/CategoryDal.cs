using CheineseSale.Models;

namespace CheineseSale.Dal
{
    public class CategoryDal:ICategoryDal
    {
        private readonly GiftsDbContext _context;
        public CategoryDal(GiftsDbContext context)
        {
            _context = context;
        }
        public List<Catagory> GetAll()
        {
            // מבצע קריאה למסד הנתונים ומחזיר את כל הקטגוריות
            return _context.Catagories.ToList();
        }
    }
}
