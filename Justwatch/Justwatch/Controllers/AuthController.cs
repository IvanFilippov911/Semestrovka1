using Justwatch.Core;
using Justwatch.Core.Attributes;
using Justwatch.Core.HttpResponse;
using Justwatch.ModelsDto;
using Justwatch.Services;
using System.Net;
using System.Text.RegularExpressions;

namespace Justwatch.Controllers;

internal class AuthController : EndpointBase
{
    [Post("loging")]
    public IHttpResponseResult Login(LoginRequestDto request)
    {
        var email = request.Email;
        var password = request.Password;

        if (UserService.ValidateLoginUser(email, password))
        {
            var token = email;
            SessionStorage.SaveSession(token, email);
            Cookie nameCookie = new Cookie("session-token", token);
            nameCookie.Path = "/";
            Context.Response.Cookies.Add(nameCookie);
            return Redirect("/home");
        }

        return Json(new { success = false, message = "Invalid login or password" });

    }

    [Post("register")]
    public IHttpResponseResult Register(RegisterRequestDto request)
    {

        Console.WriteLine($"Received request: {request.Email}, {request.Name}, {request.DateOfBirth}, {request.Password}");

        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Name) ||
            string.IsNullOrEmpty(request.DateOfBirth) || string.IsNullOrEmpty(request.Password))
        {
            return Json(new { success = false, message = "ALL fields must be completed" });
        }

        var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        if (!Regex.IsMatch(request.Email, emailPattern))
        {
            return Json(new { success = false, message = "Invalid email format" });
        }

        var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
        if (!Regex.IsMatch(request.Password, passwordPattern))
        {
            return Json(new { success = false, message = "Пароль должен содержать минимум 8 символов" +
                ", одну цифру, одну заглавную букву, одну строчную букву и один специальный символ" });
        }


        if (UserService.ValidateRegisterUser(request.Email))
        {
            return Json(new { success = false, message = "User with such email already exists" });
        }
        
        UserService.SaveUser(request.Email, request.Name, request.DateOfBirth, request.Password);
        return Redirect("/home");
    }


}
