using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DotnetBoilerPlate.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Infrastructure.Persistence.DbContext.Interfaces;

public interface IContext : IAsyncDisposable, IDisposable
{
    public DatabaseFacade Database { get; }
    
    public DbSet<City> Cities { get; }

    public DbSet<Order> Orders { get; }
    
    public DbSet<Product> Products { get; }

    public DbSet<Province> Provinces { get; }
    
    public DbSet<User> Users { get; }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}