using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GetPhone.Services;

public class BuilderUtils
{
    private WebApplicationBuilder builder;

    public BuilderUtils(WebApplicationBuilder builder)
    {
        this.builder = builder;
    }

    public void ApplicationDatabaseOptions(DbContextOptionsBuilder options)
    {
        string? connectionString;

        if (!builder.Environment.IsProduction())
        {
            connectionString = builder.Configuration.GetConnectionString("Default");
        }
        else
        {
            connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        }

        if (connectionString == null || connectionString == "") throw new InvalidOperationException("Database connection string is not configured");

        options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 3, 0)));
    }

    public void ApplicationSwaggerOptions(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Сервис для определения номера телефона",
            Contact = new OpenApiContact
            {
                Name = "Емельянов Андрей",
                Url = new Uri("https://github.com/hungrynoodlehead")
            }
        });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "JWT Токен",
            Name = "Авторизация",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement{
            {
                new OpenApiSecurityScheme{
                    Reference = new OpenApiReference{
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[]{}
            }
        });

        // Add bearer auth
    }

    public void ApplicationJWTAuthenticationOptions(JwtBearerOptions options)
    {
        options.TokenValidationParameters = AuthUtils.AppTokenValidationParameters;
    }

    public void ApplicationAuthorizationOptions(AuthorizationOptions options)
    {
        options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    }
}