using CheineseSale.Dal;
using CheineseSale.Models;
using Microsoft.AspNetCore.Identity;
using OfficeOpenXml;
using System.ComponentModel;
using System.Text.Json;

namespace CheineseSale.Dal
{
    public class RaffleDal : IRaffleDal
    {
        private readonly GiftsDbContext _context;
        private readonly string _filePathWinnersExcel = "Winners.xlsx";
        private readonly string _filePathAllTotalMoneyExcel = "AllTotalMoney.xlsx";

        public RaffleDal(GiftsDbContext context)
        {
            _context = context;
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        }

        public User GetWinner(int id)
        {
            try
            {
                var ids = (from gift in _context.UserGifts
                           where gift.GiftId == id
                           join user in _context.Users
                           on gift.UserId equals user.UserId
                           select user).ToList();

                Random random = new Random();
                int randomIndex = random.Next(0, ids.Count);
                User tmpUser = ids[randomIndex];

                // יצירת מנצח חדש
                var tmpWinner = new Winner
                {
                    GiftId = id,
                    UserId = tmpUser.UserId,
                };

                // הוספת המנצח החדש לטבלת המנצחים
                _context.Winners.Add(tmpWinner);
                _context.SaveChanges();

                // לא כולל את המנצחים ב-User
                tmpUser.Winners = null;  // המנע מלהחזיר את Winners

                return tmpUser;
            }
            catch (Exception)
            {
                // במקרה של שגיאה מחזירים null או ערך אחר שמתאים
                return null;
            }
        }

        public List<WinnersWithGifts> getAllWinners()
        {
            try
            {
                var list = (from winner in _context.Winners
                            join user in _context.Users on winner.UserId equals user.UserId
                            join gift in _context.Gifts on winner.GiftId equals gift.GiftId
                            select new WinnersWithGifts
                            {
                                Name = user.Name,
                                UserEmail = user.UserEmail,
                                UserAdress = user.UserAdress,
                                UserPhone = user.UserPhone,
                                GiftId = gift.GiftId,
                                GiftName = gift.GiftName
                            }).ToList();
                return list;
            }
            catch (Exception)
            {
                // במקרה של שגיאה מחזירים רשימה ריקה או ערך אחר
                return new List<WinnersWithGifts>();
            }
        }

        public void SaveWinnersToExcel()
        {
            try
            {
                var winners = (from winner in _context.Winners
                               join user in _context.Users on winner.UserId equals user.UserId
                               join gift in _context.Gifts on winner.GiftId equals gift.GiftId
                               select new
                               {
                                   GiftName = gift.GiftName,
                                   UserName = user.UserName,
                                   Address = user.UserAdress,
                                   Phone = user.UserPhone,
                                   Email = user.UserEmail
                               }).ToList();

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Winners");
                    worksheet.Cells[1, 1].Value = "Gift Name";
                    worksheet.Cells[1, 2].Value = "User Name";
                    worksheet.Cells[1, 3].Value = "Address";
                    worksheet.Cells[1, 4].Value = "Phone";
                    worksheet.Cells[1, 5].Value = "Email";

                    for (int i = 0; i < winners.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = winners[i].GiftName;
                        worksheet.Cells[i + 2, 2].Value = winners[i].UserName;
                        worksheet.Cells[i + 2, 3].Value = winners[i].Address;
                        worksheet.Cells[i + 2, 4].Value = winners[i].Phone;
                        worksheet.Cells[i + 2, 5].Value = winners[i].Email;
                    }

                    var file = new FileInfo(_filePathWinnersExcel);
                    package.SaveAs(file);
                }
            }
            catch (Exception)
            {
                // במקרה של שגיאה, אפשר לשמור למקום אחר או להחזיר הודעת שגיאה
            }
        }

        public void SaveTotalMoneyToExcel()
        {
            try
            {
                var allMoney = (from gift in _context.Gifts
                                select new
                                {
                                    GiftName = gift.GiftName,
                                    TotalCost = gift.Price * gift.Purchaser
                                }).ToList();

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Total Money");
                    worksheet.Cells[1, 1].Value = "Gift Name";
                    worksheet.Cells[1, 2].Value = "Total Cost";

                    for (int i = 0; i < allMoney.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = allMoney[i].GiftName;
                        worksheet.Cells[i + 2, 2].Value = allMoney[i].TotalCost;
                    }

                    var file = new FileInfo(_filePathAllTotalMoneyExcel);
                    package.SaveAs(file);
                }
            }
            catch (Exception)
            {
                // במקרה של שגיאה, אפשר לשמור למקום אחר או להחזיר הודעת שגיאה
            }
        }
    }
}
