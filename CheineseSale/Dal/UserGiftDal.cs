using CheineseSale.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CheineseSale.Dal
{
    public class UserGiftDal : IUserGiftDal
    {
        private readonly GiftsDbContext _context;

        public UserGiftDal(GiftsDbContext context)
        {
            _context = context;
        }

        public List<User> GetPurchasesForGift(int giftId)
        {
            try
            {
                var allTheBuyers = from userGift in _context.UserGifts
                                   where userGift.GiftId == giftId
                                   join user in _context.Users on userGift.UserId equals user.UserId
                                   select new User
                                   {
                                       UserName = user.UserName,
                                       Password = user.Password,
                                       Name = user.Name,
                                       UserEmail = user.UserEmail,
                                       UserAdress = user.UserAdress,
                                       UserPhone = user.UserPhone,
                                       UserRole = user.UserRole,
                                   };
                return allTheBuyers.ToList();
            }
            catch (Exception)
            {
                return new List<User>(); // החזרת רשימה ריקה במקרה של שגיאה
            }
        }

        public List<User> GetAllBuyers()
        {
            try
            {
                var result = from userGift in _context.UserGifts
                             join user in _context.Users on userGift.UserId equals user.UserId
                             select new User
                             {
                                 UserId = user.UserId,
                                 UserName = user.UserName,
                                 Password = user.Password,
                                 Name = user.Name,
                                 UserEmail = user.UserEmail,
                                 UserAdress = user.UserAdress,
                                 UserPhone = user.UserPhone,
                                 UserRole = user.UserRole,
                             };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<User>(); // החזרת רשימה ריקה במקרה של שגיאה
            }
        }

        public List<GiftsWithDonor> GetGiftsSortedByPrice()
        {
            try
            {
                var GiftsByName = (from gift in _context.Gifts
                                   join donor in _context.Donters on gift.DonterId equals donor.DonterId
                                   join category in _context.Catagories on gift.CatagoryId equals category.CatagoryId
                                   join image in _context.GiftsImages on gift.ImageId equals image.ImageId
                                   orderby gift.Price descending
                                   select new GiftsWithDonor
                                   {
                                       GiftId = gift.GiftId,
                                       GiftName = gift.GiftName,
                                       Description = gift.Description,
                                       DonorName = donor.DonterFirstName,
                                       Price = gift.Price,
                                       Purchaser = gift.Purchaser,
                                       CategoryName = category.CatagoryName,
                                       ImageGift = image.ImageName,
                                   }).ToList();
                return GiftsByName;
            }
            catch (Exception)
            {
                return new List<GiftsWithDonor>(); // החזרת רשימה ריקה במקרה של שגיאה
            }
        }

        public List<GiftsWithDonor> GetGiftsSortedByPurchaser()
        {
            try
            {
                var GiftsByName = (from gift in _context.Gifts
                                   join donor in _context.Donters on gift.DonterId equals donor.DonterId
                                   join category in _context.Catagories on gift.CatagoryId equals category.CatagoryId
                                   join image in _context.GiftsImages on gift.ImageId equals image.ImageId
                                   orderby gift.Purchaser descending
                                   select new GiftsWithDonor
                                   {
                                       GiftId = gift.GiftId,
                                       GiftName = gift.GiftName,
                                       Description = gift.Description,
                                       DonorName = donor.DonterFirstName,
                                       Price = gift.Price,
                                       Purchaser = gift.Purchaser,
                                       CategoryName = category.CatagoryName,
                                       ImageGift = image.ImageName,
                                   }).ToList();
                return GiftsByName;
            }
            catch (Exception)
            {
                return new List<GiftsWithDonor>(); // החזרת רשימה ריקה במקרה של שגיאה
            }
        }

        public string BuyGift(int id, string token)
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

                // מציאת המתנה לפי ID
                var gift = _context.Gifts.FirstOrDefault(g => g.GiftId == id);
                if (gift == null)
                {
                    throw new Exception("Gift not found.");
                }

                UserGift product = new UserGift
                {
                    UserId = user.UserId,
                    GiftId = gift.GiftId
                };

                _context.UserGifts.Add(product);

                var tmpGift = _context.Baskats.FirstOrDefault(g => g.GiftId == id && g.UserId == user.UserId);
                if (tmpGift == null)
                    return "no such gift";
                else
                    _context.Baskats.Remove(tmpGift);

                gift.Purchaser++;
                _context.Update(gift);
                _context.SaveChanges();

                return $"Gift {gift.GiftName} Bought by {user.UserName}'s ";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}"; // במקרה של שגיאה, מחזירים את ההודעה
            }
        }

        public List<GiftsWithDonor> GetAllPurchase(string token)
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

                // שליפת המשתמש מה-DB לפי שם המשתמש
                var user = _context.Users.FirstOrDefault(u => u.UserName == username);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                // שליפת כל המתנות שהמשתמש רכש
                var giftsByPurchaser = (from userGift in _context.UserGifts  // הצטרפות לטבלה המחברת בין משתמשים למתנות
                                        join gift in _context.Gifts on userGift.GiftId equals gift.GiftId
                                        join donor in _context.Donters on gift.DonterId equals donor.DonterId
                                        join category in _context.Catagories on gift.CatagoryId equals category.CatagoryId
                                        join image in _context.GiftsImages on gift.ImageId equals image.ImageId
                                        where userGift.UserId == user.UserId  // סינון לפי מזהה המשתמש
                                        select new GiftsWithDonor
                                        {
                                            GiftId = gift.GiftId,
                                            GiftName = gift.GiftName,
                                            Description = gift.Description,
                                            DonorName = donor.DonterFirstName,  // שם התורם
                                            Price = gift.Price,
                                            Purchaser = user.UserId,  // רוכש המתנה
                                            CategoryName = category.CatagoryName,
                                            ImageGift = image.ImageName
                                        }).ToList();

                return giftsByPurchaser;
            }
            catch (Exception)
            {
                return new List<GiftsWithDonor>(); // החזרת רשימה ריקה במקרה של שגיאה
            }
        }
    }
}
