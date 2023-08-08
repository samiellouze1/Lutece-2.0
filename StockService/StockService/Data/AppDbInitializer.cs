using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockService.Data.Enums;
using StockService.Models;

namespace StockService.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                if (!context.Stocks.Any()) 
                {
                    context.AddRange(new List<Stock>()
                    {
                        new Stock() {Id="1", Name="Apple",AveragePrice=200,Quantity=500},
                        new Stock() {Id="2", Name="Tesla", AveragePrice=150, Quantity=1000},
                        new Stock() {Id="3", Name="Meta", AveragePrice=120,Quantity=300},
                        new Stock() {Id="4", Name="Twitter", AveragePrice=175, Quantity=200}
                    }
                    );
                }
            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //roles insertion

                #region user
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();


                var user1username = "user1@gdp.com";
                var user1= await userManager.FindByNameAsync(user1username);
                if (user1==null)
                {
                    var newUser1 = new User()
                    {
                        Id="1",
                        UserName=user1username,
                        UserType=UserTypeEnum.Dummy,
                        Balance= 10_000
                    };
                    await userManager.CreateAsync(newUser1, "User1123@");
                    //AddToRole
                }

                var user2username = "user2@gdp.com";
                var user2 = await userManager.FindByNameAsync(user2username);
                if (user2 == null)
                {
                    var newUser2 = new User()
                    {
                        Id = "2",
                        UserName = user2username,
                        UserType = UserTypeEnum.Dummy,
                        Balance = 20_000
                    };
                    await userManager.CreateAsync(newUser2, "User2123@");
                    //AddToRole
                }

                var user3username = "user3@gdp.com";
                var user3 = await userManager.FindByNameAsync(user3username);
                if (user3 == null)
                {
                    var newUser3 = new User()
                    {
                        Id = "3",
                        UserName = user3username,
                        UserType = UserTypeEnum.Dummy,
                        Balance = 30_000
                    };
                    await userManager.CreateAsync(newUser3, "User3123@");
                    //AddToRole
                }

                var user4username = "user4@gdp.com";
                var user4 = await userManager.FindByNameAsync(user4username);
                if (user4 == null)
                {
                    var newUser4 = new User()
                    {
                        Id = "4",
                        UserName = user4username,
                        UserType = UserTypeEnum.Dummy,
                        Balance = 40_000
                    };
                    await userManager.CreateAsync(newUser4, "User4123@");
                    //AddToRole
                }
                #endregion


            }
        }
    }
}
