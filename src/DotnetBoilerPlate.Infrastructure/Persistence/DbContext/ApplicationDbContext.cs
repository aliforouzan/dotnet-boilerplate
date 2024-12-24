using Microsoft.EntityFrameworkCore;
using DotnetBoilerPlate.Domain.Entities;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext.Configuration;
using DotnetBoilerPlate.Infrastructure.Persistence.DbContext.Interfaces;

namespace DotnetBoilerPlate.Infrastructure.Persistence.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext, IContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Order> Orders { get; set; }
    
    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }
    
    public virtual DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextUserConfiguration).Assembly);

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07F891BE60");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PricePerUnit).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.Type).HasMaxLength(10);
            entity.Property(e => e.UnitCount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.DoneCount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Orders_Users");
        });
        
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07C5096B4A");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Symbol).HasMaxLength(5);
            entity.Property(e => e.SellFee).HasColumnType("decimal");
            entity.Property(e => e.BuyFee).HasColumnType("decimal");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Provinces_pk");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Slug).HasMaxLength(50);
        });
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07BA2451EC");

            entity.HasIndex(e => e.CityId, "IX_UsersCityId");

            entity.HasIndex(e => e.NationalCode, "UQ__Users__3DFA4106A35A62B2").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ__Users__85FB4E382D5F5650").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Firstname).HasMaxLength(50);
            entity.Property(e => e.Lastname).HasMaxLength(100);
            entity.Property(e => e.Level).HasDefaultValue(1);
            entity.Property(e => e.NationalCode).HasMaxLength(10);
            entity.Property(e => e.PhoneNumber).HasMaxLength(11);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Status)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasDefaultValue("Unauthorized");
            entity.Property(e => e.Type)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasDefaultValue("Customer");

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Users_Cities");
        });
    }
}