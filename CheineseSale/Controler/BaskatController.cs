using CheineseSale.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace CheineseSale.Controler
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaskatController : ControllerBase
    {
        private readonly IBaskatSrv _BaskatSrv;

        // Constructor, receives the IBaskatSrv interface
        public BaskatController(IBaskatSrv BaskatSrv)
        {
            _BaskatSrv = BaskatSrv;
        }

        
        [HttpGet("GetBasket")]
        [Authorize(Roles = "User")]
        public IActionResult GetBasket()
        {
            var basket = _BaskatSrv.GetBasket();
            if (basket == null)
            {
                return NotFound("no basket");
            }
            return Ok(basket);
        }
        
        [HttpPost("AddToCart/{id}")]
        [Authorize(Roles = "User")]
        public IActionResult AddToCart(int id)
        {
            try
            {
                Console.WriteLine($"Received ID: {id}");

                if (id <= 0)
                {
                    return BadRequest(new { message = "Invalid gift ID." });
                }

               
                string result = _BaskatSrv.AddToCart(id);

                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred", details = ex.Message });
            }
        }


       
        [HttpDelete("DeleteGiftFromBasket/{id}")]
        [Authorize(Roles = "User")]
        public IActionResult DeleteGiftFromBasket(int id)
        {
            try
            {
                Console.WriteLine($"Received ID: {id}");

                if (id <= 0)
                {
                    return BadRequest(new { message = "Invalid gift ID." });
                }


                var result = _BaskatSrv.DeleteGiftFromBasket(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An error occurred", details = ex.Message });
            }
        }
    }
}
