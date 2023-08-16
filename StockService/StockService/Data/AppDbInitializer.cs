using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockService.Data.Enums;
using StockService.Models;
using System.Runtime.Intrinsics.X86;

namespace StockService.Data
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
                        new Stock() {Id="1", Name="Apple",AveragePrice=200,Quantity=500},
                        new Stock() {Id="2", Name="Tesla", AveragePrice=150, Quantity=1000},
                        new Stock() {Id="3", Name="Meta", AveragePrice=120,Quantity=300},
                        new Stock() {Id="4", Name="Twitter", AveragePrice=175, Quantity=200}
                    });
                    context.SaveChanges();
                }
                #endregion

                #region stockunits
                var stock1 = context.Stocks.Find("1");
                var stock2 = context.Stocks.Find("2");
                var stock3 = context.Stocks.Find("3");
                var stock4 = context.Stocks.Find("4");

                var user1 = context.Users.Find("1");
                var user2 = context.Users.Find("2");
                var user3 = context.Users.Find("3");
                var user4 = context.Users.Find("4");
                if (!context.StockUnits.Any())
                {
                    StockUnitCreation(user1, stock4, 50,175, context);
                    StockUnitCreation(user1, stock3, 50,120, context);
                    StockUnitCreation(user1, stock2, 10,150, context);
                    StockUnitCreation(user1, stock1, 100,200, context);
                    StockUnitCreation(user2, stock4, 50,175, context);
                    StockUnitCreation(user2, stock3, 50,120, context);
                    StockUnitCreation(user2, stock2, 70,150, context);
                    StockUnitCreation(user2, stock1, 120,200, context);
                    StockUnitCreation(user3, stock4, 50,175, context);
                    StockUnitCreation(user3, stock3, 100,120, context);
                    StockUnitCreation(user3, stock2, 120,150, context);
                    StockUnitCreation(user3, stock1, 130,200, context);
                    StockUnitCreation(user4, stock4, 50,175, context);
                    StockUnitCreation(user4, stock3, 100,120, context);
                    StockUnitCreation(user4, stock2, 800,150, context);
                    StockUnitCreation(user4, stock1, 50,200, context);
                }
                #endregion

                #region originalorders
                if (!context.OriginalOrders.Any())
                {
                    SellingOriginalOrderCreation(user1, stock1, 10, 150, context);
                    SellingOriginalOrderCreation(user2, stock1, 20, 170, context);
                    BuyingOriginalOrderCreation(user1, stock2, 10, 150, context);
                    BuyingOriginalOrderCreation(user2, stock2, 20, 170, context);
                }
                #endregion
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
                var user1 = await userManager.FindByNameAsync(user1username);
                if (user1 == null)
                {
                    var newUser1 = new User()
                    {
                        Id = "1",
                        UserName = user1username,
                        UserType = UserTypeEnum.Dummy,
                        Balance = 10_000
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
        private static void StockUnitCreation(User user, Stock stock, int quantity,double price, AppDbContext context)
        {
            var stockunits = new List<StockUnit>();
            for (int i = 0; i < quantity; i++)
            {
                stockunits.Add
                (
                        new StockUnit() 
                        {
                            User = user,
                            Stock = stock,
                            StockUnitStatus = StockUnitStatusEnum.InStock,
                            DateBought = DateTime.Now,
                            PriceBought = price
                        }
                    );
            }
            context.StockUnits.AddRange(stockunits);
            context.SaveChanges();
        }
        private static void SellingOriginalOrderCreation(User user, Stock stock, int quantity, double price, AppDbContext context)
        {
            var stockunits = context.StockUnits.Where(su => su.Stock == stock && su.User == user).OrderBy(su => su.DateBought).ToList();
            for (int i = 0; i < quantity; i++)
            {
                stockunits[i].StockUnitStatus = StockUnitStatusEnum.InMarket;
                context.SaveChanges();
            }
            var orders = new List<Order>();
            for (int i = 0; i < quantity; i++)
            {
                orders.Add(new Order() { OrderStatus = OrderStatusEnum.Active });
            }
            var result = context.Add(new OriginalOrder()
            {
                Stock = stock,
                Price = price,
                OriginalQuantity = quantity,
                User = user,
                OriginalOrderType = OriginalOrderTypeEnum.Sell,
                RemainingQuantity = quantity,
                Orders = orders
            });
            context.SaveChanges();
        }
        private static void BuyingOriginalOrderCreation(User user, Stock stock, int quantity, double price, AppDbContext context)
        {
            var orders = new List<Order>();
            for (int i = 0; i < quantity; i++)
            {
                orders.Add(new Order()
                {
                    OrderStatus = OrderStatusEnum.Active,
                });
            }
            var result = context.Add(new OriginalOrder()
            {
                Stock = stock,
                Price = price,
                OriginalQuantity = quantity,
                User = user,
                OriginalOrderType = OriginalOrderTypeEnum.Buy,
                RemainingQuantity = quantity,
                Orders = orders
            });
            context.SaveChanges();
        }
    }
}
