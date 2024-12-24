using Microsoft.EntityFrameworkCore;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext.Interfaces;

namespace DotnetBoilerPlate.Infrastructure.Persistence.DbContext.Factory;

public class ContextFactory : IContextFactory
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public ContextFactory(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public IContext Create()
    {
        return _dbContextFactory.CreateDbContext();
    }
}