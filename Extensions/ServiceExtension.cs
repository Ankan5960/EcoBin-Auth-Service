using System.Data;
using System.Text;
using EcoBin_Auth_Service.Extensions.Helpers;
using EcoBin_Auth_Service.Helpers;
using EcoBin_Auth_Service.Repositories;
using EcoBin_Auth_Service.Repositories.Contracts;
using EcoBin_Auth_Service.Services;
using EcoBin_Auth_Service.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;

namespace EcoBin_Auth_Service.Extensions;

public static class ServiceExtension
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
        });

    public static void ConfigureDBConnection(this IServiceCollection services, IConfiguration config) =>
        services.AddScoped<IDbConnection>(db => new NpgsqlConnection(config.GetConnectionString("DefaultConnection")));

    public static void InjectRepository(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void InjectService(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    public static void InjectJwtHelper(this IServiceCollection services) =>
        services.AddSingleton<IJwtHelper, JwtHelper>();

    public static void ConfigureDapperMapping(this IServiceCollection services) =>
        DapperHelper.SetSnakeCaseMapping();

    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration) =>
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? string.Empty)),
                    ClockSkew = TimeSpan.Zero
                };
            });

    public static void ConfigureAuthorization(this IServiceCollection services) =>
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy =>
            policy.RequireClaim("roleName", "Admin"));
            options.AddPolicy("CollectorOrHigher", policy =>
            policy.RequireClaim("roleName", "Admin", "Collector"));
        });

    public static void ConfigureSwaggerGen(this IServiceCollection services) =>
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your JWT token"
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
                    new List<string>()
                }
            });
        });
}