using ProbabilityService.Repo.IRepo;
using ProbabilityService.Repo.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region crud
builder.Services.AddScoped<IStockRepo, StockRepo>();
builder.Services.AddScoped<IProbabilityDistributionRepo, ProbabilityDistributionUnitRepo>();
builder.Services.AddScoped<IStockTraceRepo, StockTraceRepo>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
