using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext.Interfaces;
using DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;
using DotnetBoilerPlate.Shared.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DotnetBoilerPlate.Infrastructure.Persistence.Repository;

public class UserRepository : Repository<User>, IUserRepository
{

    public async Task<User?> GetByNationalCodeAsync(string nationalCode)
    {
        User? user = await FirstOrDefaultAsync(u => u.NationalCode == nationalCode);

        return user;
    }

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
    {
        User? user = await FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

        return user;
    }

    public async Task<User?> GetByIdAsync(UserId userId)
    {
        var user = await FirstOrDefaultAsync(u => u.Id == userId);

        return user;
    }

    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}