using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StockService.Data;
using StockService.Data.Extensions;
using StockService.Models;
using StockService.Repo.IRepo;
using StockService.Repo.Repo;
using StockService.Services.IServices;
using StockService.Services.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, ApplicationUserClaimsPrincipalFactory>();


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("Development");
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
    Console.WriteLine("Using In Memory Database");
}
else
{
    Console.WriteLine("Production");
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionString")));
    Console.WriteLine("using sql server database");
}

#region addidentity
// Add Identity services
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

// Register UserManager and SignInManager
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<SignInManager<User>>();

#endregion

#region Authentication
builder.Services.AddMemoryCache();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});
#endregion


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stock API", Version = "v1" });
});

#region crud

builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOriginalOrderRepo, OriginalOrderRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IStockUnitRepo, StockUnitRepo>();
builder.Services.AddScoped<IStockRepo, StockRepo>();


#endregion

#region services

builder.Services.AddScoped<ICreateOriginalOrderService, CreateOriginalOrderService>();
#endregion

#region automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
AppDbInitializer.Seed(app);

app.Run();
