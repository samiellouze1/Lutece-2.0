using ProbabilityService.AsyncDataServices;
using ProbabilityService.EventProcessing;
using ProbabilityService.Repo.IRepo;
using ProbabilityService.Repo.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHostedService<MessageBusSubscriber>();
#region eventprocessor
builder.Services.AddScoped<IEventProcessor, EventProcessor>();
#endregion
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
