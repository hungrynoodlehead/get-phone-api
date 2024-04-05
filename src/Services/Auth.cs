// TODO: ну сделать
/*using System.IdentityModel.Tokens.Jwt;
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

    public SymmetricSecurityKey SecurityKey;

    public Auth(IWebHostEnvironment environment, IConfiguration configuration)
    {
        this.environment = environment;
        this.configuration = configuration;

        string? keyString;

        if(!environment.IsDevelopment()){
            keyString = configuration["JWT:SecurityKey"];
        } else {
            keyString = Environment.GetEnvironmentVariable("JWT_SECURITY_KEY");
        }

        if(keyString == null || keyString == ""){
            throw new InvalidOperationException("JWT security key is not configured");
        } else {
            SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        }
    }

    public const string ISSUER = "GetPhoneInfoServer";
    public const string AUDIENCE = "GetPhoneInfoClient";

    public TokenValidationParameters AppTokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = ISSUER,
        ValidateAudience = true,
        ValidAudience = AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = SecurityKey,
        ValidateIssuerSigningKey = true
    };

    public string getToken(int userId, string email, string role)
    {

        JwtSecurityToken jwtOptions = new JwtSecurityToken(
            issuer: ISSUER,
            audience: AUDIENCE,
            claims: new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            },
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
            signingCredentials: new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(jwtOptions);
    }
}*/