using CheineseSale.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CheineseSale.Dal
{
    public class DonterDal : IDonterDal
    {
        private readonly GiftsDbContext _context;
        public DonterDal(GiftsDbContext context)
        {
            _context = context;
        }

        public List<Gift> GetDonorGifts(int id)
        {
            try
            {
                var GetDG = _context.Donters
                    .Where(d => d.DonterId == id)
                    .Select(d => d.Gifts)
                    .FirstOrDefault();

                if (GetDG == null)
                {
                    throw new Exception("תורם עם ID זה לא נמצא");
                }

                return GetDG.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת המתנות של התורם", ex);
            }
        }

        public void add(Donter donter)
        {
            try
            {
                _context.Donters.Add(donter);
                _context.SaveChanges();

                if (donter.Gifts != null)
                {
                    foreach (var gift in donter.Gifts)
                    {
                        gift.DonterId = donter.DonterId; // מקשר את המתנה לתורם
                        _context.Gifts.Add(gift);
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בהוספת התורם והמתנות", ex);
            }
        }

        public List<Donter> GetDonters()
        {
            try
            {
                return _context.Donters.ToList(); // מחזיר את כל התורמים
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת התורמים", ex);
            }
        }

        public void DeleteDonter(int id)
        {
            try
            {
                var donter = _context.Donters
                                     .Include(d => d.Gifts) // לכלול את המתנות כדי לבדוק אם יש
                                     .FirstOrDefault(d => d.DonterId == id);

                if (donter == null)
                {
                    throw new Exception("לא נמצא תורם עם מזהה זה.");
                }

                foreach (var gift in donter.Gifts.ToList())
                {
                    _context.Gifts.Remove(gift);
                }

                _context.Donters.Remove(donter);
                _context.SaveChanges(); // שמירת השינויים
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה במחיקת התורם", ex);
            }
        }

        public void Update(Donter donter)
        {
            try
            {
                var existingDonter = _context.Donters.FirstOrDefault(d => d.DonterId == donter.DonterId);

                if (existingDonter == null)
                {
                    throw new Exception("לא נמצא תורם עם מזהה זה");
                }

                // עדכון פרטי התורם
                existingDonter.DonterFirstName = donter.DonterFirstName;
                existingDonter.DonterLastName = donter.DonterLastName;
                existingDonter.DonterPhon = donter.DonterPhon;
                existingDonter.DonterMail = donter.DonterMail;

                // עדכון המתנות של התורם אם יש צורך
                if (donter.Gifts != null)
                {
                    foreach (var gift in donter.Gifts)
                    {
                        var existingGift = _context.Gifts.FirstOrDefault(g => g.GiftId == gift.GiftId);
                        if (existingGift != null)
                        {
                            existingGift.GiftName = gift.GiftName;
                            existingGift.Price = gift.Price;
                            existingGift.Description = gift.Description;
                            existingGift.CatagoryId = gift.CatagoryId;
                            existingGift.ImageId = gift.ImageId;
                        }
                    }
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בעדכון התורם והמתנות", ex);
            }
        }

        public Donter? GetDonterById(int donterId)
        {
            try
            {
                return _context.Donters.FirstOrDefault(d => d.DonterId == donterId);
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת התורם לפי מזהה", ex);
            }
        }

        public List<Donter> GetDontersByName(string DonterFirstName)
        {
            try
            {
                return _context.Donters
                    .Where(d => d.DonterFirstName.Contains(DonterFirstName))
                    .ToList(); // מחזיר את התורמים עם שם התואם לחיפוש
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת התורמים לפי שם", ex);
            }
        }

        public List<Donter> GetDontersByEmail(string DonterMail)
        {
            try
            {
                return _context.Donters
                    .Where(d => d.DonterMail.Contains(DonterMail)) // סינון לפי חלק מהמייל
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת התורמים לפי מייל", ex);
            }
        }

        // פונקציה לשירות
        public List<Donter> GetDontersByGiftName(string giftName)
        {
            try
            {
                var donters = _context.Donters
                                      .Where(d => d.Gifts.Any(g => g.GiftName.Contains(giftName))) // חיפוש לפי שם המתנה
                                      .ToList();

                return donters;
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת התורמים לפי שם מתנה", ex);
            }
        }
    }
}
