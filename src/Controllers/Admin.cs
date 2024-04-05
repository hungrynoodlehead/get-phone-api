using GetPhone.Database;
using GetPhone.Database.Models;
using GetPhone.Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using GetPhone.Services;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("[controller]/[action]")]
public class AdminController : Controller {
    private readonly IRepository<Admin> adminRepository;
    public AdminController(IRepository<Admin> adminRepository){
        this.adminRepository = adminRepository;
    }

    public class AdminLoginForm {
        public string username {get; set;} = "";
        public string password {get; set;} = "";
    }

    [HttpGet]
    public IActionResult login(AdminLoginForm form){
        if(form.username == "" || form.password == "") return BadRequest();

        string hash = AuthUtils.Hash(form.password);
        Admin? admin = adminRepository.Get(a => a.Username == form.username && a.PasswordHash == hash);
        if(admin == null) return NotFound();

        JwtSecurityToken jwtOptions = new JwtSecurityToken(
            issuer: AuthUtils.ISSUER,
            audience: AuthUtils.AUDIENCE,
            claims: new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString())
            },
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
            signingCredentials: new SigningCredentials(AuthUtils.SecurityKey(), SecurityAlgorithms.HmacSha256)
        );
        return Json(new{
            accessToken=new JwtSecurityTokenHandler().WriteToken(jwtOptions)
        });
    }
}