using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext;
using DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;

namespace DotnetBoilerPlate.Infrastructure.Persistence.Repository;

public class ProvinceRepository : Repository<Province>, IProvinceRepository
{
    public ProvinceRepository(ApplicationDbContext context) : base(context)
    {
    }
}