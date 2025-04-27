using CheineseSale.Models;
using CheineseSale.Service;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RegistrController : ControllerBase
{
    private readonly IregistrSrv _registrSrv;

    public RegistrController(IregistrSrv registrSrv)
    {
        _registrSrv = registrSrv;
    }

    [HttpPost]
    public IActionResult UserRegister(User user)
    {
        User registeredUser = _registrSrv.UserRegister(user);
        if (registeredUser != null)
        {
            return Ok(registeredUser); // מחזירים את אובייקט המשתמש שנרשם בהצלחה
        }

        return BadRequest(new { message = "User register not successfully" }); // החזרת הודעת שגיאה אם המשתמש כבר קיים
    }
}
