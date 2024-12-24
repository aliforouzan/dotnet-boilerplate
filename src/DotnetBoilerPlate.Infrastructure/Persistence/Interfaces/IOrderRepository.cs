using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Shared.Types;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    IQueryable<Order> GetAllAsync(UserId userId);
    Task<Order?> GetByIdAsync(int orderId, UserId userId);
}