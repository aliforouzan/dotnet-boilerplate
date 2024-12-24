using Microsoft.EntityFrameworkCore;
using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext.Interfaces;
using DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Infrastructure.Persistence.Repository;

public class CityRepository : Repository<City>, ICityRepository
{
    public CityRepository(ApplicationDbContext context) : base(context)
    {
    }
}