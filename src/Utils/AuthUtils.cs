// TODO: ну сделать
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace GetPhone.Services;

public static class AuthUtils
{
    private static IConfiguration configuration;
    private static string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

    static AuthUtils()
    {
        configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
    }

    public const string ISSUER = "GetPhoneInfoServer";
    public const string AUDIENCE = "GetPhoneInfoClient";
    public static SymmetricSecurityKey SecurityKey()
    {
        string? keyString;

        if (environment == "Development")
        {
            keyString = configuration["JWT:SecurityKey"];
        }
        else
        {
            keyString = Environment.GetEnvironmentVariable("JWT_SECURITY_KEY");
        }

        if (keyString == null || keyString == "")
        {
            throw new InvalidOperationException("JWT security key is not configured");
        }
        else
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        }
    }

    public static TokenValidationParameters AppTokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = ISSUER,
        ValidateAudience = true,
        ValidAudience = AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = SecurityKey(),
        ValidateIssuerSigningKey = true
    };

    public static string Hash(string input)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hash = SHA256.HashData(inputBytes);
        return Convert.ToHexString(hash).ToLower();
    }
}