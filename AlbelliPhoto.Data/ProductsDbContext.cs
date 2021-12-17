using AlbelliPhoto.Abstraction.Entities;
using AlbelliPhoto.Data.Configurations;

using Microsoft.EntityFrameworkCore;

namespace AlbelliPhoto.Data
{
    public class ProductsDbContext : DbContext
    {
        /// <summary>
        /// constructor with options
        /// </summary>
        /// <param name="options"></param>
        public ProductsDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new OrdersMappings());
            modelBuilder.ApplyConfiguration(new OrderItemsMappings());
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}