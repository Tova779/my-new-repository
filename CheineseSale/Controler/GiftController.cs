using CheineseSale.Models;
using CheineseSale.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheineseSale.Controler
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private readonly IgiftSrvice _giftSrvice;
        public GiftController(IgiftSrvice giftSrvice)
        {
            _giftSrvice = giftSrvice;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add([FromBody] GiftsWithDonor gift)
        {
            try
            {
                _giftSrvice.Add(gift);
                return CreatedAtAction(nameof(Get), new { id = gift.GiftId }, gift);

            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });  // מחזירים תגובת שגיאה עם ההודעה הרלוונטית
            }
        }
       
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult Get()
        {
            var gift = _giftSrvice.Get();
            return Ok(gift);
        }
        [HttpGet("by-name/{giftName}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetGiftsByName(string giftName)
        {
            try
            {
                var gifts = _giftSrvice.GetGiftsByName(giftName) ?? new List<GiftsWithDonor>(); // אם התוצאה היא null, מחזירים רשימה ריקה

                if (!gifts.Any())  // אם הרשימה ריקה
                {
                    return NotFound($"לא נמצאו מתנות בשם {giftName}");
                }

                return Ok(gifts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה פנימית: {ex.Message}");
            }
        }

        [HttpGet("by-id/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetGiftsById(int id)
        {
            try
            {
                var gifts = _giftSrvice.GetGiftsById(id);
                if (gifts == null)
                {
                    return NotFound($"לא נמצאו מתנות בשם {id}");
                }

                return Ok(gifts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה פנימית: {ex.Message}");
            }
        }
        [HttpGet("by-donter/{donterFirstName}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetGiftsByDonterName(string donterFirstName)
        {
            try
            {
                var gifts = _giftSrvice.GetGiftsByDonterName(donterFirstName);
                if (gifts == null || !gifts.Any())
                {
                    return NotFound($"לא נמצאו מתנות עבור תורם {donterFirstName} ");
                }

                return Ok(gifts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה פנימית: {ex.Message}");
            }
        }
        [HttpGet("search-by-purchasers")]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<Gift>> SearchGiftsByPurchasers([FromQuery] int minPurchasers)
        {
            var gifts = _giftSrvice.SearchGiftsByPurchasers(minPurchasers);

            if (gifts == null || gifts.Count == 0)
            {
                return NotFound("לא נמצאו מתנות עם מספר רוכשים כזה.");
            }

            return Ok(gifts);
        }
        
        [HttpPut("updateGift")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(GiftsWithDonor gift)
        {
            _giftSrvice.Update(gift);
            return Ok(gift);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _giftSrvice.Delete(id);
            return NoContent();
        }


    }
}

