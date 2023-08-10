using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockService.Models;

namespace StockService.Data
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<OriginalOrder> OriginalOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<StockUnit> StockUnits { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public AppDbContext (DbContextOptions<AppDbContext> opt): base(opt)
        {

        }
        protected override void OnModelCreating (ModelBuilder builder)
        {
            #region relationships
            builder.Entity<StockUnit>().HasOne(su => su.User).WithMany(u => u.StockUnits).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<StockUnit>().HasOne(su => su.Stock).WithMany(s => s.StockUnits).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Order>().HasOne(o => o.OriginalOrder).WithMany(oo => oo.Orders).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<OriginalOrder>().HasOne(oo => oo.User).WithMany(u => u.OriginalOrders).OnDelete(DeleteBehavior.Cascade).HasForeignKey(oo=>oo.UserId);
            builder.Entity<OriginalOrder>().HasOne(o=>o.Stock).WithMany(u => u.OriginalOrders).OnDelete(DeleteBehavior.Cascade).HasForeignKey(oo => oo.StockId);
            #endregion

            #region doubles
            builder.Entity<OriginalOrder>().Property(oo => oo.Price).HasPrecision(6, 2);
            builder.Entity<User>().Property(u=>u.Balance).HasPrecision(6, 2);
            builder.Entity<Order>().Property(o=>o.ExecutedPrice).HasPrecision(6, 2);
            builder.Entity<Stock>().Property(s=>s.AveragePrice).HasPrecision(6, 2);
            #endregion

            base.OnModelCreating(builder);
        }
    }
}
