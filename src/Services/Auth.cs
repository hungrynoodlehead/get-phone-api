using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace GetPhone.Services;

public class Auth : IAuth
{
    private readonly IWebHostEnvironment environment;
    private readonly IConfiguration configuration;

    public Auth(IWebHostEnvironment environment, IConfiguration configuration)
    {
        this.environment = environment;
        this.configuration = configuration;
    }


    public string getToken(int userId, string role)
    {
        string? keyString;

        if(!environment.IsDevelopment()){
            keyString = configuration["JWT:SecurityKey"];
        } else {
            keyString = Environment.GetEnvironmentVariable("JWT_SECURITY_KEY");
        }

        if(keyString == null || keyString == ""){
            throw new InvalidOperationException("JWT security key is not configured");
        }


        /*jwtOptions.Claims.Append(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
        jwtOptions.Claims.Append(new Claim(ClaimTypes.Role, role));
        return new JwtSecurityTokenHandler().WriteToken(jwtOptions);*/
    }



    /*class AuthOptions
    {
        public const string ISSUER = "GetPhoneInfoServer";
        public const string AUDIENCE = "GetPhoneInfoClient";
        private const string key = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }

    public TokenValidationParameters AppTokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.ISSUER,
        ValidateAudience = true,
        ValidAudience = AuthOptions.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };

    public string Hash(string input)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hash = SHA256.HashData(inputBytes);
        return Convert.ToHexString(hash).ToLower();
    }

    public static class KeyOptions
    {
        public static int keySize = 256;
        public static int iterations = 10000;
    }
    JwtSecurityToken jwtOptions = new JwtSecurityToken(
        issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: new List<Claim>(),
        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
    );*/
}