using CheineseSale.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheineseSale.Dal
{
    public class giftsWithWinnersDal : IgiftsWithWinnersDal
    {
        private readonly GiftsDbContext _context;

        public giftsWithWinnersDal(GiftsDbContext context)
        {
            _context = context;
        }

        public List<giftsWithWinners> GetGiftWitWinners()
        {
            try
            {
                var allGifts = _context.Gifts
                    .Include(g => g.Winners)
                    .ThenInclude(ug => ug.User)
                    .Include(gift => gift.Image)
                    .Select(gift => new giftsWithWinners
                    {
                        GiftName = gift.GiftName,
                        ImageGift = gift.Image != null ? gift.Image.ImageName : null,
                        Winners = gift.Winners.Select(ug => ug.User.UserName).ToList()
                    }).ToList();

                return allGifts;
            }
            catch (Exception)
            {
                // במקרה של שגיאה, מחזירים רשימה ריקה או ניתן להחזיר שגיאה מותאמת אישית
                return new List<giftsWithWinners>();
            }
        }
    }
}
