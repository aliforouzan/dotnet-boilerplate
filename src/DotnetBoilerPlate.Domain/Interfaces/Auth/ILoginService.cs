using DotnetBoilerPlate.Domain.Entities;

namespace DotnetBoilerPlate.Domain.Interfaces.Auth;

public interface ILoginService
{
    bool Login(User? user, string password);
}