using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.Core.Models.Storage;

namespace Storage.DataBase.DataContext
{
    public class StorageDbContext : DbContext
    {
        private readonly ILogger _logger;

        public StorageDbContext(DbContextOptions<StorageDbContext> opt, ILogger<StorageDbContext> logger) : base(opt)
        {
            _logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.LogTo((mes) => _logger.LogInformation(mes), minimumLevel: LogLevel.Debug);
#else
            optionsBuilder.LogTo((mes) => _logger.LogInformation(mes), minimumLevel: LogLevel.Information);
#endif
        }

        public DbSet<ProductCategory> ProductCategories { get; init; }
        public DbSet<Product> Products { get; init; }
        public DbSet<StorageItem> AllItems { get; init; }

        public DbSet<Area> Areas { get; init; }
        public DbSet<SubArea> SubAreas { get; init; }
        public DbSet<Cell> Cells { get; init; }
        public DbSet<CellType> CellTypes { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CellTypeEntityConfiguration).Assembly);
    }

#region EntityConfiguration
    public class CellTypeEntityConfiguration : IEntityTypeConfiguration<CellType>
    {
        public void Configure(EntityTypeBuilder<CellType> builder)
        {
            builder
                .HasIndex(x => x.Name)
                .IsUnique();
        }
    }
    public class ProductCategoryEntityConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder
                .HasIndex(x => x.Name)
                .IsUnique();
        }
    }
    public class CellEntityConfiguration : IEntityTypeConfiguration<Cell>
    {
        public void Configure(EntityTypeBuilder<Cell> builder)
        {
            builder
                .HasOne(p => p.SubArea)
                .WithMany(v => v.Cells)
                .HasForeignKey(x => x.SubAreaId);

            builder.Property(x => x.SubAreaLengthX);
            builder.Property(x => x.SubAreaHeigthZ);
            builder.Property(x => x.SubAreaWidthY);
        }
    }
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasOne(p => p.ProductCategory)
                .WithMany(v => v.Products)
                .HasForeignKey(x => x.ProductCategoryId);
        }
    }
    public class SubAreaEntityConfiguration : IEntityTypeConfiguration<SubArea>
    {
        public void Configure(EntityTypeBuilder<SubArea> builder)
        {
            builder
                .HasOne(p => p.Area)
                .WithMany(v => v.SubAreas)
                .HasForeignKey(x => x.AreaId);

            builder
                .HasOne(p => p.CellType)
                .WithMany(v => v.SubAreas)
                .HasForeignKey(x => x.CellTypeId);
        }
    }
    public class StorageItemEntityConfiguration : IEntityTypeConfiguration<StorageItem>
    {
        public void Configure(EntityTypeBuilder<StorageItem> builder)
        {
            builder
                .HasOne(p => p.Product)
                .WithMany(v => v.Items)
                .HasForeignKey(x => x.ProductId);

            builder
                .HasOne(p => p.Cell)
                .WithMany(p => p.Items)
                .HasForeignKey(x => x.CellId);
        }
    }
#endregion
}
