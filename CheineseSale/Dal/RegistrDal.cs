using CheineseSale.Dal;
using CheineseSale.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

public class RegistrDal : IregistrDal
{
    private readonly GiftsDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher;

    public RegistrDal(GiftsDbContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
    }

    public User UserRegister(User user)
    {
        try
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null.");

            // בדיקות תקינות
            if (string.IsNullOrEmpty(user.UserName))
                throw new ArgumentException("UserName is required.");
            if (user.UserName.Length < 4 || user.UserName.Length > 20)
                throw new ArgumentException("UserName must be between 4 and 20 characters.");

            if (string.IsNullOrEmpty(user.UserEmail) || !IsValidEmail(user.UserEmail))
                throw new ArgumentException("Valid email is required.");
            if (string.IsNullOrEmpty(user.UserPhone) || !IsValidPhone(user.UserPhone))
                throw new ArgumentException("Valid phone number is required.");
            if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters long.");

            // בדיקה אם שם המשתמש או המייל כבר קיימים
            User? tmpUser = _context.Users.FirstOrDefault(u => u.UserName == user.UserName || u.UserEmail == user.UserEmail);
            if (tmpUser != null)
            {
                throw new ArgumentException("User with the same username or email already exists.");
            }

            // בדיקה אם השם הפרטי מכיל רק אותיות
            if (string.IsNullOrEmpty(user.Name) || !IsValidName(user.Name))
                throw new ArgumentException("First name must contain only letters.");

            var password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = password;

            _context.Users.Add(user);
            _context.SaveChanges();
            return user; // מחזיר את המשתמש לאחר שנרשם בהצלחה
        }
        catch (ArgumentException ex)
        {
            // במקרה של טעות פרמטרים, אפשר להחזיר null או להתאים את החזרת הערך
            return null;
        }
        catch (Exception)
        {
            // כל שגיאה אחרת שתקרה במהלך הרישום
            return null;
        }
    }

    // פונקציות עזר לבדיקת תקינות המייל והפלאפון
    private bool IsValidEmail(string email)
    {
        var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        return emailRegex.IsMatch(email);
    }

    private bool IsValidPhone(string phone)
    {
        var phoneRegex = new Regex(@"^\d{10}$"); // 10 ספרות
        return phoneRegex.IsMatch(phone);
    }

    private bool IsValidName(string name)
    {
        var nameRegex = new Regex(@"^[a-zA-Zא-ת]+$"); // רק אותיות אנגליות ועבריות (ללא רווחים)
        return nameRegex.IsMatch(name);
    }

}
