// using System.Text;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
// using Orchard.Core.Configuration;

using AutoMapper.EquivalencyExpression;
using Microsoft.EntityFrameworkCore;
using Orchard.Data.Cache;
using Orchard.Data.Contexts;
using Orchard.Data.Repositories.Implementations;
using Orchard.Data.Repositories.Interfaces;
using Orchard.Grpc.Services;
using Orchard.Models.Mappers;
using Orchard.Services.Domain;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

var connectionString = builder.Configuration.GetConnectionString("orchard");
// var jwtSettingsSection = builder.Configuration.GetSection(JwtSettings.JwtSettingsSection);
// var redisSettingsSection = builder.Configuration.GetSection(RedisSettings.RedisSettingsSection);
// var orchardConfiguration = builder.Configuration.Get<OrchardConfiguration>();

// string oauthSecret = orchardConfiguration!.JwtSettings!.Key;
// string issuer = orchardConfiguration.JwtSettings.Issuer;
// string audience = orchardConfiguration.JwtSettings.Audience;
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IFruitRepository, FruitRepository>();
builder.Services.AddScoped<IOrchardService, OrchardService>();

builder.Services.AddDbContext<OrchardDbContext>((optionsAction) =>
{
    optionsAction.UseNpgsql(connectionString);
});

builder.Services.AddAutoMapper((config) =>
{
    config.AddCollectionMappers();
    config.AddMaps(
        typeof(FruitProfile), 
        typeof(BasketProfile), 
        typeof(BasketFruitProfile), 
        typeof(UserProfile));
});
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
// {
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(oauthSecret)),
//         ValidIssuer = issuer,
//         ValidAudience = audience,
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true
//     };
// });

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseAuthentication();
//app.UseAuthorization();

app.MapGrpcService<OrdersService>();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();