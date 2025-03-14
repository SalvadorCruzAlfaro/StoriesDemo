using Stories.Application.Features.GetBestStories;
using Stories.Domain.Configuration;
using Stories.Domain.Interfaces;
using Stories.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Register HackerNewsApi configuration
builder.Services.Configure<HackerNewsApiSettings>(builder.Configuration.GetSection("HackerNewsApi"));

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetBestStoriesQuery).Assembly));

// Register infrastructure services
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IHackerNewsService, HackerNewsService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
