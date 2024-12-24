using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Shared.Types;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByNationalCodeAsync(string nationalCode);
    Task<User?> GetByPhoneNumberAsync(string phoneNumber);
    Task<User?> GetByIdAsync(UserId userId);
}