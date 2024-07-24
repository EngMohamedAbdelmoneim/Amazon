using System.Reflection;
using Amazon.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Amazon.Core.DBContext
{
    public class AmazonDbContext : DbContext
    {
        public AmazonDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
    }
}
