using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DotnetBoilerPlate.Domain.Auth;
using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Domain.Interfaces;
using DotnetBoilerPlate.Domain.Interfaces.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotnetBoilerPlate.Domain.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration config)
    {
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
    }
    public string CreateToken(User? user, TokenType tokenType)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = tokenType == TokenType.Access 
                ? DateTime.Now.AddSeconds(double.Parse(_config["JWT:AccessTokenExpireDuration"]))
                : DateTime.Now.AddSeconds(double.Parse(_config["JWT:RefreshTokenExpireDuration"])),
            SigningCredentials = creds,
            Issuer = _config["JWT:Issuer"],
            Audience = _config["JWT:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}