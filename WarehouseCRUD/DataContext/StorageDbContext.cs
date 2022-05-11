using Microsoft.EntityFrameworkCore;
using WarehouseCRUD.Storage.Models;
using WarehouseCRUD.Storage.Models.Storage;

namespace WarehouseCRUD.Storage.DataContext
{
    public class StorageDbContext : DbContext
    {
        public StorageDbContext(DbContextOptions<StorageDbContext> opt) : base(opt)
        { }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StorageItem> AllItems { get; set; }

        public DbSet<Area> Areas { get; set; }
        public DbSet<SubArea> SubAreas { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<CellType> CellTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildProduct();
            BuildStorageItem();
            BuildSubArea();
            BuildCell();

            void BuildStorageItem()
            {
                modelBuilder.Entity<StorageItem>()
                    .HasOne(p => p.Product)
                    .WithMany(v => v.Items)
                    .HasForeignKey(x => x.ProductId);

                modelBuilder.Entity<StorageItem>()
                    .Property(s => s.State)
                    .HasConversion<string>();

                modelBuilder.Entity<StorageItem>()
                    .HasOne(p => p.Cell)
                    .WithMany(p => p.Items)
                    .HasForeignKey(x => x.CellId);
            }
            void BuildSubArea()
            {
                modelBuilder.Entity<SubArea>()
                    .HasOne(p => p.Area)
                    .WithMany(v => v.SubAreas)
                    .HasForeignKey(x => x.AreaId);
            }
            void BuildProduct()
            {
                modelBuilder.Entity<Product>()
                    .HasOne(p => p.ProductCategory)
                    .WithMany(v => v.Products)
                    .HasForeignKey(x => x.ProductCategoryId);
            }
            void BuildCell()
            {
                modelBuilder.Entity<Cell>()
                    .HasOne(p => p.CellType)
                    .WithMany(v => v.Cells)
                    .HasForeignKey(x => x.CellTypeId);

                modelBuilder.Entity<Cell>()
                    .HasOne(p => p.SubArea)
                    .WithMany(v => v.Cells)
                    .HasForeignKey(x => x.SubAreaId);

                modelBuilder.Entity<Cell>()
                    .HasOne(p => p.Area)
                    .WithMany(v => v.Cells)
                    .HasForeignKey(x => x.AreaId);

                modelBuilder.Entity<Cell>(b =>
               {
                   b.Property(x => x.Name);
                   b.Property(x => x.SubAreaLenghtX);
                   b.Property(x => x.SubAreaHeightY);
                   b.Property(x => x.SubAreaWidthZ);
               });

            }
        }
    }
}
