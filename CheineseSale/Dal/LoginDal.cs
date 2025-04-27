using CheineseSale.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CheineseSale.Dal
{
    public class LoginDal:ILoginDal
    {
        private readonly GiftsDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        public LoginDal(GiftsDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        public User LoginOfUser(string UserName, string Password)
        {
            try
            {
                User? user = _context.Users.FirstOrDefault(x => x.UserName == UserName);
               
                if (user != null)
                {
                    var s = _passwordHasher.VerifyHashedPassword(user, user.Password, Password) == PasswordVerificationResult.Success;
                    if (s)
                    {
                        return user;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                // ניתן להוסיף כאן לוגים של השגיאה לצורך דיבוג
                Console.WriteLine($"Error during login: {ex.Message}");
                
                // אם אתה רוצה להחזיר null במקרה של שגיאה או טיפול אחר, זה המקום
                return null;
            }
        }

    }
}

