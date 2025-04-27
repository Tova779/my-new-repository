using CheineseSale.Dal;
using CheineseSale.Models;

namespace CheineseSale.Service
{
    public class registrSrv : IregistrSrv
    {
        private readonly IregistrDal _registrDal;
        private readonly ILogger<registrSrv> _logger;

        public registrSrv(IregistrDal registrDal, ILogger<registrSrv> logger)
        {
            _registrDal = registrDal;
            _logger = logger;
        }

        public User UserRegister(User user)
        {
            try
            {
                return _registrDal.UserRegister(user);
                _logger.LogInformation("user 👉 {user.UserName} add successfully", user.UserName);
            }
            catch (Exception ex)
            {
                // אתה יכול לטפל בשגיאה פה (לוגים, רימטול, או החזרת הודעה מותאמת)
                throw new Exception("There was an error during user registration.", ex);
            }
        }
    }
}
