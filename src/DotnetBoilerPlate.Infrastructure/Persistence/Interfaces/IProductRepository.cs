using DotnetBoilerPlate.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetBySymbolAsync(string symbol);
}