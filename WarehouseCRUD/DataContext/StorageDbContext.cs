using Microsoft.EntityFrameworkCore;
using WarehouseCRUD.Storage.Models;

namespace WarehouseCRUD.Storage.DataContext
{
    public class StorageDbContext : DbContext
    {
        public StorageDbContext(DbContextOptions<StorageDbContext> opt) : base(opt)
        { }
        public DbSet<Product> Products { get; set; }
    }
}
