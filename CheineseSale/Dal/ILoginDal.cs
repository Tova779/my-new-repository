using CheineseSale.Models;

namespace CheineseSale.Dal
{
    public interface ILoginDal
    {
        public User LoginOfUser(string UserName, string Password);
    }
}
