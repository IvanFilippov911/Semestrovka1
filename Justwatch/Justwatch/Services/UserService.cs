using Justwatch.Core.Config;
using Justwatch.Models;
using Justwatch.ORM;
using System.Security.Cryptography;
using System.Text;


namespace Justwatch.Services;
public static class UserService
{
    private static readonly ORMContext<User> _dbContext = new ORMContext<User>(AppConfig.ConnectionString);

    public static void SaveUser(string email, string name, string dateOfBirth, string password)
    {
        var user = new User
        {
            Email = email,
            Name = name,
            DateOfBirth = dateOfBirth,
            PasswordHash = HashPassword(password)
        };

        _dbContext.Create(user, "users");
    }

    public static bool ValidateLoginUser(string email, string password)
    {
        var users = _dbContext.ReadAll<User>("users");
        var user = users.FirstOrDefault(u => u.Email == email);
        return user != null && VerifyPassword(password, user.PasswordHash);
    }

    public static bool ValidateRegisterUser(string email)
    {
        var users = _dbContext.ReadAll<User>("users");
        var user = users.FirstOrDefault(u => u.Email == email);

        return user != null;
    }

    public static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    private static bool VerifyPassword(string password, string storedHash)
    {
        var hash = HashPassword(password);
        return hash == storedHash;
    }
}

