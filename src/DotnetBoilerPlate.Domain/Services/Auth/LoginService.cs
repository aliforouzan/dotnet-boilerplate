using Microsoft.Extensions.Configuration;
using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Domain.Extentions;
using DotnetBoilerPlate.Domain.Interfaces.Auth;
using System.Security.Authentication;

namespace DotnetBoilerPlate.Domain.Services.Auth;

public class LoginService : ILoginService
{
    private readonly IConfiguration _config;

    public LoginService(IConfiguration config)
    {
        _config = config;
    }
    
    public bool Login(User? user, string password)
    {
        if (user is null)
        {
            throw new AuthenticationException("نام کاربری یا رمز عبور اشتباه است");
        }

        if (user.IsBlocked())
        {
            throw new AuthenticationException("حساب کاربری شما مسدود شده است. لطفا با پشتیبانی تماس بگیرید.");
        }

        var hashedPassword = Pbkdf2HashExtension.Hash(password, _config["Hashing:Salt"]);
        if (!user.Password.Equals(hashedPassword))
        {
            throw new AuthenticationException("نام کاربری یا رمز عبور اشتباه است");
        }

        return true;
    }
}