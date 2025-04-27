using CheineseSale.Models;
using CheineseSale.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheineseSale.Controler
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class DonterController : ControllerBase
    {
        private readonly IdonterSrv _donterSrv;
        public DonterController(IdonterSrv donterSrv)
        {
            _donterSrv = donterSrv;
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add([FromBody] Donter donter)
        {
            if (donter == null)
            {
                return BadRequest("הנתונים אינם תקינים.");
            }

            try
            {
                // קוראים לשירות שיבצע את הפעולה
                _donterSrv.add(donter);

                // במקרה של הצלחה, מחזירים את התורם שנוסף עם סטטוס 201
                return CreatedAtAction(nameof(GetDonters), new { id = donter.DonterId }, donter);
            }
            catch (Exception ex)
            {
                // במקרה של שגיאה, מחזירים שגיאה עם הודעה
                return StatusCode(500, $"שגיאה במערכת: {ex.Message}");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetDonters()
        {
            try
            {
                var donters = _donterSrv.GetDonters();

                if (donters == null || !donters.Any())
                {
                    return NotFound("לא נמצאו תורמים.");
                }

                return Ok(donters);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה במערכת: {ex.Message}");
            }
        }

        [HttpGet("{id}/gifts")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetDonorGifts(int id)
        {
            try
            {
                var gifts = _donterSrv.GetDonorGifts(id);
                if (gifts == null || !gifts.Any())
                {
                    return NotFound($"לא נמצאו מתנות עבור תורם עם מזהה {id}");
                }

                return Ok(gifts); // מחזיר את המתנות (200 OK)
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה פנימית: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Donter> GetDonterById(int id)
        {
            var donter = _donterSrv.GetDonterById(id);
            if (donter == null)
            {
                return NotFound();
            }

            return Ok(donter);
        }
        // סינון לפי שם תורם - שינוי הנתיב כך שיתאים לשם באופן ייחודי
        [HttpGet("by-name/{DonterFirstName}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetDontersByName(string DonterFirstName)
        {
            try
            {
                var donters = _donterSrv.GetDontersByName(DonterFirstName);
                if (donters == null || !donters.Any())
                {
                    return NotFound($"לא נמצא תורם בשם {DonterFirstName}");
                }

                return Ok(donters); // מחזיר את התורמים (200 OK)
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה פנימית: {ex.Message}");
            }
        }

        // סינון לפי מייל תורם - שינוי הנתיב כך שיתאים לשם באופן ייחודי
        [HttpGet("by-email/{DonterMail}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetDontersByEmail(string DonterMail)
        {
            try
            {
                var donters = _donterSrv.GetDontersByEmail(DonterMail);
                if (donters == null || !donters.Any())
                {
                    return NotFound($"לא נמצא תורם עם המייל {DonterMail}");
                }

                return Ok(donters); // מחזיר את התורמים (200 OK)
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה פנימית: {ex.Message}");
            }
        }
        [HttpGet("by-gift/{giftName}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetDontersByGiftName(string giftName)
        {
            try
            {
                var donters = _donterSrv.GetDontersByGiftName(giftName);
                if (donters == null || !donters.Any())
                {
                    return NotFound($"לא נמצאו תורמים עם המתנה {giftName}");
                }

                return Ok(donters); // מחזיר את התורמים
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה פנימית: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteDonter(int id)
        {
            try
            {
                _donterSrv.DeleteDonter(id); // פנייה לשירות לצורך מחיקת התורם
                return NoContent(); // מחזיר תשובת 204 (No Content) כשהמחיקה הצליחה
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // אם הייתה שגיאה (כמו לא נמצא תורם), מחזיר תשובת 404 עם הודעת השגיאה
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateDonter(int id, [FromBody] Donter donter)
        {
            if (id != donter.DonterId)
            {
                return BadRequest("מזהה התורם לא תואם");
            }

            try
            {
                _donterSrv.Update(donter);
                return NoContent(); // בהצלחה - לא מחזירים תוכן
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה בעדכון התורם: {ex.Message}");
            }
        }
    }
}
