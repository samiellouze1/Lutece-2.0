using ProbabilityService.Models;

namespace ProbabilityService.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                #region stocks 
                if (!context.Stocks.Any())
                {
                    context.AddRange(new List<Stock>()
                    {
                        new Stock() {Id="1", StockId="1",AveragePrice=200},
                        new Stock() {Id="2", StockId="2", AveragePrice=150},
                        new Stock() {Id="3", StockId="3", AveragePrice=120},
                        new Stock() {Id="4", StockId="4", AveragePrice=175}
                    });
                    context.SaveChanges();
                }
                #endregion
            }
        }
    }
}
