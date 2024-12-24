using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Shared.Types;

namespace DotnetBoilerPlate.Infrastructure.Persistence.DbContext.Configuration;

public class DbContextUserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<UserId.EfCoreValueConverter>();
    }
}