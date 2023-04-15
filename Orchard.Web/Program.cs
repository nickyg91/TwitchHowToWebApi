using AutoMapper.EquivalencyExpression;
using Microsoft.EntityFrameworkCore;
using Orchard.Data.Contexts;
using Orchard.Data.Repositories.Implementations;
using Orchard.Data.Repositories.Interfaces;
using Orchard.Models.Mappers;
using Orchard.Services.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("orchard");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrchardDbContext>(optionsAction =>
{
    if (builder.Environment.IsDevelopment())
    {
        optionsAction.EnableDetailedErrors();
        optionsAction.EnableSensitiveDataLogging();
    }
    optionsAction.UseNpgsql(connectionString, builder =>
    {
        builder.MigrationsAssembly("Orchard.Web");
    });
});

builder.Services.AddScoped<IFruitRepository, FruitRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IOrchardService, OrchardService>();
builder.Services.AddAutoMapper((builder) =>
{
    builder.AddCollectionMappers();
    builder.AddMaps(typeof(FruitProfile), typeof(BasketProfile), typeof(BasketFruitProfile));
});

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
