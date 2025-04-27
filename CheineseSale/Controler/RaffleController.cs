using CheineseSale.Models;
using CheineseSale.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheineseSale.Controler
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaffleController : ControllerBase
    {
        private readonly IRaffleSrv _rafflesServices;
        public RaffleController(IRaffleSrv raffleService)
        {
            _rafflesServices = raffleService;
        }
        [HttpGet("getWinner/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetWinner(int id)
        {
            var user = _rafflesServices.GetWinner(id);
            if (user == null)
            {
                return NotFound("Something went wrong, try again.");
            }
            return Ok(user);
        }
        [HttpGet("getAllWinners")]
        [Authorize(Roles = "User, Admin")]
        public ActionResult<List<WinnersWithGifts>> GetAllWinners()
        {
            try
            {
                // קריאה לפונקציה ששולפת את כל הזוכים
                var winners = _rafflesServices.getAllWinners();

                // אם לא נמצאו זוכים, מחזירים תשובה עם קוד 404
                if (winners == null || winners.Count == 0)
                {
                    return NotFound("No winners found.");
                }

                // מחזירים את הרשימה עם קוד 200 (הצלחה)
                return Ok(winners);
            }
            catch (System.Exception ex)
            {
                // טיפול בשגיאות
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    

        [HttpGet("saveWinnersToExcel")]
        public IActionResult SaveWinnersToExcel()
        {
            _rafflesServices.SaveWinnersToExcel();
            return Ok("Winners saved to Excel successfully.");
        }
        [HttpGet("saveTotalMoneyToExcel")]
        public IActionResult SaveTotalMoneyToExcel()
        {
            _rafflesServices.SaveTotalMoneyToExcel();
            return Ok("Total money saved to Excel successfully.");
        }
    }

}
