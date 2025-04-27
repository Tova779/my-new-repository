using CheineseSale.Dal;
using CheineseSale.Models;

namespace CheineseSale.Service
{
    public class loginSrv : IloginSrv
    {
        private readonly ILoginDal _loginDal;

        public loginSrv(ILoginDal loginDal)
        {
            _loginDal = loginDal;
        }

        public User LoginOfUser(string UserName, string Password)
        {
            try
            {
                return _loginDal.LoginOfUser(UserName, Password);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה - החזרת שגיאה מותאמת אישית
                throw new Exception("An error occurred while logging in the user.", ex);
            }
        }
    }
}
