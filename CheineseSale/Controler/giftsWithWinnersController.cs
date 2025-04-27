using CheineseSale.Models;
using CheineseSale.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheineseSale.Controler
{
    [Route("api/[controller]")]
    [ApiController]
    public class giftsWithWinnersController : ControllerBase
    {
        private readonly IgiftsWithWinnersSrv _giftsWithWinnersSrv;
        public giftsWithWinnersController(IgiftsWithWinnersSrv giftsWithWinnersSrv)
        {
            _giftsWithWinnersSrv = giftsWithWinnersSrv;
        }
        [HttpGet("with-winners")]
        [Authorize(Roles = "User, Admin")]
        public ActionResult<List<giftsWithWinners>> GetGiftWithWinners()
        {
            try
            {
                var giftsWithWinners = _giftsWithWinnersSrv.GetGiftWitWinners();

                if (giftsWithWinners == null || giftsWithWinners.Count == 0)
                {
                    return NotFound(new { message = "No gifts found with winners." });
                }

                return Ok(giftsWithWinners);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the data.", details = ex.Message });
            }
        }
    }
}

