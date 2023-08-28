using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProbabilityService.AsyncDataServices;
using ProbabilityService.Data;
using ProbabilityService.EventProcessing;
using ProbabilityService.Repo.IRepo;
using ProbabilityService.Repo.Repo;
using ProbabilityService.SyncDataServices.Http;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHangfire(configuration => configuration
.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
.UseSimpleAssemblyNameTypeSerializer()
.UseRecommendedSerializerSettings()
.UseMemoryStorage());

builder.Services.AddHangfireServer();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemProb"));
builder.Services.AddControllers();
builder.Services.AddHostedService<MessageBusSubscriber>();
#region eventprocessor
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
#endregion

#region swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Probability API", Version = "v1" });
});
#endregion

#region crud
builder.Services.AddScoped<IStockRepo, StockRepo>();
builder.Services.AddScoped<IProbabilityDistributionRepo, ProbabilityDistributionUnitRepo>();
builder.Services.AddScoped<IStockTraceRepo, StockTraceRepo>();
#endregion

builder.Services.AddHttpClient<IHttpProbabilityDataClient, HttpProbabilityDataClient>();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();
AppDbInitializer.Seed(app);
app.Run();
