using CheineseSale.Models;
using CheineseSale.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CheineseSale.Controler
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGiftController : ControllerBase
    {
        private readonly IUserGiftSrv _userGiftSrv;

        public UserGiftController(IUserGiftSrv userGiftSrv)
        {
            _userGiftSrv = userGiftSrv;
        }

        // הפונקציה מחזירה את כל הרכישות עבור מתנה ספציפית לפי מזהה
        [HttpGet("purchases/{giftId}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult GetPurchasesForGift(int giftId)
        {
            try
            {
                // קריאה לשירות כדי לשלוף את הרוכשים עבור המתנה
                var allTheBuyers = _userGiftSrv.GetPurchasesForGift(giftId);

                if (allTheBuyers == null || !allTheBuyers.Any())
                {
                    // אם לא נמצאו רוכשים
                    return NotFound($"לא נמצאו רכישות עבור המתנה עם מזהה {giftId}");
                }

                // אם נמצאו רוכשים
                return Ok(allTheBuyers);
            }
            catch (Exception ex)
            {
                // במקרה של שגיאה כללית
                return StatusCode(500, $"שגיאה פנימית: {ex.Message}");
            }
        }
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult GetAllBuyers()
        {
            try
            {
                var Buyers = _userGiftSrv.GetAllBuyers();

                if (Buyers == null || !Buyers.Any())
                {
                    return NotFound("לא נמצאו תורמים.");
                }

                return Ok(Buyers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה במערכת: {ex.Message}");
            }
        }
        [HttpGet("gifts/SortedByPric")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult GetGiftsSortedByPrice()
        {
            try
            {
                var sortedGifts = _userGiftSrv.GetGiftsSortedByPrice();

                if (sortedGifts == null || !sortedGifts.Any())
                {
                    return NotFound("לא נמצאו מתנות.");
                }

                return Ok(sortedGifts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה במערכת: {ex.Message}");
            }
        }
        [HttpGet("gifts/sortedPurchaser")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult GetGiftsSortedByPurchaser()
        {
            try
            {
                var sortedGifts = _userGiftSrv.GetGiftsSortedByPurchaser();

                if (sortedGifts == null || !sortedGifts.Any())
                {
                    return NotFound("לא נמצאו מתנות.");
                }

                return Ok(sortedGifts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאה במערכת: {ex.Message}");
            }
        }
        [HttpPost("BuyGift/{id}")]
       
        [Authorize(Roles = "User, Admin")]
        public IActionResult BuyGift(int id)
        {
            try
            {
                Console.WriteLine($"Received ID: {id}");

                if (id <= 0)
                {
                    return BadRequest(new { message = "Invalid gift ID." });
                }


                string result = _userGiftSrv.BuyGift(id);

                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred", details = ex.Message });
            }
        }
        [HttpGet("GetAllPurchase")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult GetAllPurchase()
        {
                var result = _userGiftSrv.GetAllPurchase();

                return Ok(result);
            }

        }
    }

