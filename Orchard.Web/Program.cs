using System.Text;
using AutoMapper.EquivalencyExpression;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Orchard.Core.Configuration;
using Orchard.Core.Services;
using Orchard.Core.Services.Email;
using Orchard.Data.Cache;
using Orchard.Data.Contexts;
using Orchard.Data.Repositories.Implementations;
using Orchard.Data.Repositories.Interfaces;
using Orchard.Grpc;
using Orchard.Models.Authentication;
using Orchard.Models.Mappers;
using Orchard.Services.Domain;
using Orchard.Web.ApplicationServices;
using Orchard.Web.Authentication;

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
string environmentUrl = "https://localhost:5002";
    
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
builder.Services.AddHttpContextAccessor();
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
var smtpSettingsSection = builder.Configuration.GetSection(SmtpSettings.SmtpSettingsKey);
var settings = builder.Configuration.Get<OrchardConfiguration>();

builder.Services.Configure<JwtSettings>(jwtSettingsSection);
builder.Services.Configure<RedisSettings>(redisSettingsSection);
builder.Services.Configure<SmtpSettings>(smtpSettingsSection);

builder.Services.AddScoped<IFruitRepository, FruitRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IOrchardService, OrchardService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrchardFruitService, OrchardFruitService>();
builder.Services.AddScoped<IFruitInventoryManagementService, FruitInventoryManagementService>();
builder.Services.AddTransient<TokenGenerator>();
builder.Services.AddSingleton<PasswordHashService>();
builder.Services.AddScoped<IOrdersApplicationService, OrdersApplicationService>();

builder.Services
    .AddTransient<IEmailService, EmailService>((_) => 
        new EmailService(settings.SmtpSettings, environmentUrl));

builder.Services.AddTransient<GrpcChannel>((_) => GrpcChannel.ForAddress("https://localhost:7195"));

builder.Services.AddTransient<Orders.OrdersClient>((options) =>
{
    var channel = options.GetService<GrpcChannel>();
    return new Orders.OrdersClient(channel);
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

builder.Services.AddTransient<IAuthenticatedUser, AuthenticatedUser>(provider =>
{
    var user =
        provider.GetService<IHttpContextAccessor>()!.HttpContext!.User;
    return new AuthenticatedUser(user);
});

builder.Services.AddSingleton<ICacheService, CacheService>();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
