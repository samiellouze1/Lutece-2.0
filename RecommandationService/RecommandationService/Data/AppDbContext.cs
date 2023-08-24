using Microsoft.EntityFrameworkCore;
using RecommandationService.Models;
namespace RecommandationService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region relationships
            builder.Entity<RecommandationRequest>().HasOne(rr=>rr.RecommandationResponse).WithOne(rr=>rr.RecommandationRequest).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RecommandationResponse>().HasMany(rr => rr.BuyingOriginalOrders).WithOne(boy => boy.RecommandationResponse).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<BuyingOriginalOrderStock>().HasOne(boos=>boos.BuyingOriginalOrder).WithMany(boo=>boo.BuyingOriginalOrderStocks).OnDelete(DeleteBehavior.Cascade);    
            builder.Entity<BuyingOriginalOrderStock>().HasOne(boos=>boos.Stock).WithMany(s=>s.BuyingOriginalOrdersStock).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ProbabilityDistributionUnit>().HasOne(pdu=>pdu.Stock).WithMany(s=>s.ProbabilityDistributionUnits).OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region doubles
            builder.Entity<RecommandationRequest>().Property(rr => rr.a).HasPrecision(6, 2);
            builder.Entity<RecommandationRequest>().Property(rr=>rr.b).HasPrecision(6, 2);
            builder.Entity<BuyingOriginalOrder>().Property(boo=>boo.Price).HasPrecision(6, 2);
            builder.Entity<ProbabilityDistributionUnit>().Property(pdu=>pdu.Price).HasPrecision(6, 2);
            builder.Entity<ProbabilityDistributionUnit>().Property(pdu=>pdu.Probability).HasPrecision(6, 2);
            #endregion

            base.OnModelCreating(builder);
        }
    }
}
