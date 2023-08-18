using Microsoft.EntityFrameworkCore;
using ProbabilityService.Models;

namespace ProbabilityService.Data
{
    public class AppDbContext:DbContext
    {
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<StockTrace> StockTraces { get; set;}
        public virtual DbSet<ProbabilityDistributionUnit> ProbabilityDistributionUnits { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> opt) :base(opt)
        {
            
        }
        protected override void OnModelCreating (ModelBuilder builder)
        {
            #region relationships
            builder.Entity<StockTrace>().HasOne(st=>st.Stock).WithMany(s=>s.StockTraces).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ProbabilityDistributionUnit>().HasOne(pdu=>pdu.Stock).WithMany(s=>s.ProbabilityDistributionUnits).OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region doubles
            builder.Entity<StockTrace>().Property(st => st.Price).HasPrecision(6, 2);
            builder.Entity<ProbabilityDistributionUnit>().Property(st => st.Price).HasPrecision(6, 2);
            builder.Entity<ProbabilityDistributionUnit>().Property(st => st.Probability).HasPrecision(6, 2);
            #endregion
            base.OnModelCreating(builder);
        }
    }
}
