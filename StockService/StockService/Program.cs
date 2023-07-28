using Microsoft.EntityFrameworkCore;
using StockService.Data;
using StockService.Data.IRepo;
using StockService.Data.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine("Using Local Db");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("localdb")));

builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOriginalOrderRepo, OriginalOrderRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IStockUserRepo, StockUserRepo>();
builder.Services.AddScoped<IStockRepo, StockRepo>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
