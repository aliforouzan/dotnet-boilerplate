using System;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> GetRepository<T>() where T : class;
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}