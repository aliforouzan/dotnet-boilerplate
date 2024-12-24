using DotnetBoilerPlate.Domain.Auth;
using DotnetBoilerPlate.Domain.Entities;

namespace DotnetBoilerPlate.Domain.Interfaces.Auth;

public interface ITokenService
{
    string CreateToken(User? user, TokenType tokenType);
}