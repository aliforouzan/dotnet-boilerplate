using Microsoft.EntityFrameworkCore;
using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext.Interfaces;
using DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;
using DotnetBoilerPlate.Shared.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Infrastructure.Persistence.Repository;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public IQueryable<Order> GetAllAsync(UserId userId)
    {
        var orders = FindAsync(o => o.UserId == userId);

        return orders;
    }

    public Task<Order?> GetByIdAsync(int orderId, UserId userId)
    {
        var order = FirstOrDefaultAsync(x => x.Id == orderId && x.UserId == userId);
        
        return order;
    }

    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }
}