using CheineseSale.Models;

namespace CheineseSale.Service
{
    public interface IloginSrv
    {
        public User LoginOfUser(string UserName, string Password);
    }
}
