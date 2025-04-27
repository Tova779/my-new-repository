using CheineseSale.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CheineseSale.Dal
{
    public class BaskatDal : IBaskat
    {
        private readonly GiftsDbContext _context;
        public BaskatDal(GiftsDbContext context)
        {
            _context = context;
        }

        public List<GiftsWithDonor> GetBasket(string token)
        {
            try
            {
                // פענוח ה-Token
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // חילוץ שם המשתמש מתוך ה-Token
                var username = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                if (username == null)
                {
                    throw new Exception("Invalid token.");
                }

                // קישור שם המשתמש ל-User
                var user = _context.Users.FirstOrDefault(u => u.UserName == username);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                var allBasket = (from b in _context.Baskats
                                 where b.UserId == user.UserId
                                 join g in _context.Gifts on b.GiftId equals g.GiftId
                                 join donor in _context.Donters on g.DonterId equals donor.DonterId
                                 join category in _context.Catagories on g.CatagoryId equals category.CatagoryId
                                 join image in _context.GiftsImages on g.ImageId equals image.ImageId
                                 select new GiftsWithDonor
                                 {
                                     GiftId = g.GiftId,
                                     GiftName = g.GiftName,
                                     Description = g.Description,
                                     DonorName = donor.DonterFirstName,
                                     Price = g.Price,
                                     CategoryName = category.CatagoryName,
                                     Purchaser = g.Purchaser,
                                     ImageGift = image.ImageName,
                                 }).ToList();

                return allBasket;
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה, לדוגמה: לוג או החזרת שגיאה מותאמת אישית
                throw new Exception("An error occurred while retrieving the basket.", ex);
            }
        }

        public string AddToCart(int id, string token)
        {
            try
            {
                Console.WriteLine($"Received Token: {token}");  // הדפס את ה-token בקונסול בשרת

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // חילוץ שם המשתמש מתוך ה-Token
                var username = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                Console.WriteLine($"Extracted Username: {username}");

                if (username == null)
                {
                    throw new Exception("Invalid token.");
                }

                // קישור שם המשתמש ל-User
                var user = _context.Users.FirstOrDefault(u => u.UserName == username);
                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                var gift = _context.Gifts.FirstOrDefault(g => g.GiftId == id);
                if (gift == null)
                {
                    throw new Exception("Gift not found.");
                }

                Baskat basket = new Baskat
                {
                    UserId = user.UserId,
                    GiftId = gift.GiftId
                };

                _context.Baskats.Add(basket);
                _context.SaveChanges();

                return $"Gift {gift.GiftName} added to {user.UserName}'s cart.";
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה, לדוגמה: לוג או החזרת שגיאה מותאמת אישית
                throw new Exception("An error occurred while adding the gift to the cart.", ex);
            }
        }

        public string DeleteGiftFromBasket(int id, string token)
        {
            try
            {
                // פענוח ה-Token
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // חילוץ שם המשתמש מתוך ה-Token
                var username = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                if (username == null)
                {
                    throw new Exception("Invalid token.");
                }

                // קישור שם המשתמש ל-User
                var user = _context.Users.FirstOrDefault(u => u.UserName == username);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                var tmpGift = _context.Baskats.FirstOrDefault(g => g.GiftId == id && g.UserId == user.UserId);
                if (tmpGift != null)
                {
                    _context.Baskats.Remove(tmpGift);
                    _context.SaveChanges();

                    return $"Gift {id} deleted from basket of user {user.UserName}.";
                }
                return "No such gift found in the basket.";
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה, לדוגמה: לוג או החזרת שגיאה מותאמת אישית
                throw new Exception("An error occurred while deleting the gift from the basket.", ex);
            }
        }
    }
}
