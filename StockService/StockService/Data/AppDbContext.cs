using Microsoft.EntityFrameworkCore;
using StockService.Models;

namespace StockService.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> opt): base(opt)
        {

        }
        protected override void OnModelCreating (ModelBuilder builder)
        {
            builder.Entity<Order>().Property(o => o.Price).HasPrecision(4, 3);
            base.OnModelCreating(builder);
        }
        public DbSet<Order> Orders { get; set; }
    }
}
