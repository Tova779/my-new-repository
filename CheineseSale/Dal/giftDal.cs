using CheineseSale.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace CheineseSale.Dal
{
    public class giftDal : IgiftDal
    {
        private readonly GiftsDbContext _context;

        public giftDal(GiftsDbContext context)
        {
            _context = context;
        }

        public void add(GiftsWithDonor gift)
        {
            try
            {
                var d = _context.Donters.FirstOrDefault(d => d.DonterFirstName == gift.DonorName);
                var c = _context.Catagories.FirstOrDefault(c => c.CatagoryName == gift.CategoryName);
                var i = _context.GiftsImages.FirstOrDefault(i => i.ImageName == gift.ImageGift);

                if (d == null || c == null || i == null)
                {
                    throw new Exception("הנתונים שהוזנו לא תקינים. אחד או יותר מהאובייקטים לא נמצאו במסד נתונים.");
                }

                var g = new Gift
                {
                    GiftName = gift.GiftName,
                    CatagoryId = c.CatagoryId,
                    Price = gift.Price,
                    Purchaser = gift.Purchaser,
                    DonterId = d.DonterId,
                    Description = gift.Description,
                    ImageId = i.ImageId,
                };

                _context.Gifts.Add(g);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בהוספת המתנה", ex);
            }
        }

        public void Update(GiftsWithDonor gift)
        {
            try
            {
                var d = _context.Donters.FirstOrDefault(d => d.DonterFirstName == gift.DonorName);
                var c = _context.Catagories.FirstOrDefault(c => c.CatagoryName == gift.CategoryName);
                var i = _context.GiftsImages.FirstOrDefault(i => i.ImageName == gift.ImageGift);
                var tmpGift = _context.Gifts.FirstOrDefault(g => g.GiftId == gift.GiftId);

                if (tmpGift == null)
                {
                    throw new ArgumentNullException("המתנה אינה קיימת");
                }

                tmpGift.GiftId = gift.GiftId;
                tmpGift.GiftName = gift.GiftName;
                tmpGift.Description = gift.Description;
                tmpGift.DonterId = d.DonterId;
                tmpGift.Price = gift.Price;
                tmpGift.CatagoryId = c.CatagoryId;
                tmpGift.ImageId = i.ImageId;
                tmpGift.Purchaser = gift.Purchaser;

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בעדכון המתנה", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                Gift? gift = _context.Gifts.Find(id);

                if (gift != null)
                {
                    _context.Gifts.Remove(gift);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה במחיקת המתנה", ex);
            }
        }

        public IEnumerable<Gift> Get()
        {
            try
            {
                return _context.Gifts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת המתנות", ex);
            }
        }

        public List<GiftsWithDonor> GetGifts()
        {
            try
            {
                var giftsWithDonor = from gift in _context.Gifts
                                     join donor in _context.Donters on gift.DonterId equals donor.DonterId
                                     join category in _context.Catagories on gift.CatagoryId equals category.CatagoryId
                                     join image in _context.GiftsImages on gift.ImageId equals image.ImageId
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
                                     };
                return giftsWithDonor.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת המתנות עם תורם", ex);
            }
        }

        public List<GiftsWithDonor> GetGiftsByName(string giftName)
        {
            try
            {
                var GiftsByName = (from gift in _context.Gifts
                                   where gift.GiftName.Contains(giftName)
                                   join donor in _context.Donters on gift.DonterId equals donor.DonterId
                                   join category in _context.Catagories on gift.CatagoryId equals category.CatagoryId
                                   join image in _context.GiftsImages on gift.ImageId equals image.ImageId
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
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת מתנות לפי שם", ex);
            }
        }

        public GiftsWithDonor GetGiftsById(int id)
        {
            try
            {
                var GiftsById = (from gift in _context.Gifts
                                 where gift.GiftId == id
                                 join donor in _context.Donters on gift.DonterId equals donor.DonterId
                                 join category in _context.Catagories on gift.CatagoryId equals category.CatagoryId
                                 join image in _context.GiftsImages on gift.ImageId equals image.ImageId
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
                                 }).FirstOrDefault();
                return GiftsById;
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת מתנה לפי מזהה", ex);
            }
        }

        public List<GiftsWithDonor> GetGiftsByDonterName(string donterFirstName)
        {
            try
            {
                var donters = _context.Donters
                                      .Where(d => d.DonterFirstName.Contains(donterFirstName))
                                      .ToList(); // מבצעים שליפה למסד הנתונים

                if (donters == null || donters.Count == 0)
                {
                    return new List<GiftsWithDonor>();
                }

                var gifts = _context.Gifts
                                    .Include(g => g.Catagory)
                                    .Include(g => g.Image)
                                    .ToList();

                var giftsWithDonor = (from gift in gifts
                                      join donor in donters on gift.DonterId equals donor.DonterId
                                      join category in _context.Catagories on gift.CatagoryId equals category.CatagoryId
                                      join image in _context.GiftsImages on gift.ImageId equals image.ImageId
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

                return giftsWithDonor;
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת מתנות לפי שם תורם", ex);
            }
        }

        public List<GiftsWithDonor> SearchGiftsByPurchasers(int minPurchasers)
        {
            try
            {
                var GiftsByPurchasers = (from gift in _context.Gifts
                                   where gift.Purchaser == minPurchasers
                                   join donor in _context.Donters on gift.DonterId equals donor.DonterId
                                   join category in _context.Catagories on gift.CatagoryId equals category.CatagoryId
                                   join image in _context.GiftsImages on gift.ImageId equals image.ImageId
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
                                   });
                return GiftsByPurchasers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("שגיאה בשליפת מתנות לפי קונים", ex);
            }
        }
    }
}
