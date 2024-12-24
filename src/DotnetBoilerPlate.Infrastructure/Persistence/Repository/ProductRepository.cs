using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext;
using DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Infrastructure.Persistence.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public async Task<Product?> GetBySymbolAsync(string symbol)
    {
        var product = await FirstOrDefaultAsync(p => p.Symbol == symbol);

        return product;
    }

    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }
}