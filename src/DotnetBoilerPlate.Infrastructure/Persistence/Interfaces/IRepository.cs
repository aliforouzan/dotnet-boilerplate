using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotnetBoilerPlate.Infrastructure.Persistence.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int? id);
    IQueryable<T> GetAllAsync();
    IQueryable<T> FindAsync(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void SaveChangesAsync();
}

