using Microsoft.EntityFrameworkCore;

namespace StockService.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                //here you can seed your data. leave it to later

            }
        }
    }
}
