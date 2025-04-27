using CheineseSale.Models;
using CheineseSale.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheineseSale.Controler
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IloginSrv _loginSrv;

        // בנאי שמקבל את ה-SRV שלך
        public LoginController(IloginSrv loginSrv)
        {
            _loginSrv = loginSrv;
        }
        [HttpGet]

        public IActionResult GetUser(string UserName, string UserPassword)
        {
            var user = _loginSrv.LoginOfUser(UserName, UserPassword);
            if (user == null)
            {
                return Ok(new { message = "משתמש לא קיים, יש להירשם." });

            }
            return Ok(user);
        }
    }
}