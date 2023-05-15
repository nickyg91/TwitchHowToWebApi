using System.Text;
using AutoMapper.EquivalencyExpression;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Orchard.Core.Configuration;
using Orchard.Core.Services;
using Orchard.Data.Cache;
using Orchard.Data.Contexts;
using Orchard.Data.Repositories.Implementations;
using Orchard.Data.Repositories.Interfaces;
using Orchard.Models.Authentication;
using Orchard.Models.Mappers;
using Orchard.Services.Domain;
using Orchard.Web.Authentication;
using Orchard.Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("orchard");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();

var orchardConfiguration = builder.Configuration.Get<OrchardConfiguration>();
string oauthSecret = orchardConfiguration!.JwtSettings!.Key;
string issuer = orchardConfiguration.JwtSettings.Issuer;
string audience = orchardConfiguration.JwtSettings.Audience;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(oauthSecret)),
        ValidIssuer = issuer,
        ValidAudience = audience,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

builder.Services.AddDbContext<OrchardDbContext>(optionsAction =>
{
    if (builder.Environment.IsDevelopment())
    {
        optionsAction.EnableDetailedErrors();
        optionsAction.EnableSensitiveDataLogging();
    }
    optionsAction.UseNpgsql(connectionString, settings =>
    {
        settings.MigrationsAssembly("Orchard.Web");
    });
});
var jwtSettingsSection = builder.Configuration.GetSection(JwtSettings.JwtSettingsSection);
var redisSettingsSection = builder.Configuration.GetSection(RedisSettings.RedisSettingsSection);
builder.Services.Configure<JwtSettings>(jwtSettingsSection);
builder.Services.Configure<RedisSettings>(redisSettingsSection);
builder.Services.AddScoped<IFruitRepository, FruitRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IOrchardService, OrchardService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<TokenGenerator>();
builder.Services.AddSingleton<PasswordHashService>();

builder.Services.AddAutoMapper((settings) =>
{
    settings.AddCollectionMappers();
    settings.AddMaps(
        typeof(FruitProfile), 
        typeof(BasketProfile), 
        typeof(BasketFruitProfile), 
        typeof(UserProfile));
});

builder.Services.AddTransient<IAuthenticatedUser, AuthenticatedUser>(provider =>
{
    var user =
        provider.GetService<IHttpContextAccessor>()!.HttpContext!.User;
    return new AuthenticatedUser(user);
});


builder.Services.AddSingleton<ICacheService, CacheService>();

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
