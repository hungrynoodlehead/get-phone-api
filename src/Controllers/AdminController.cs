using GetPhone.Database;
using GetPhone.Database.Models;
using GetPhone.Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using GetPhone.Services;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

    [HttpPost]
    public IActionResult Login(AdminLoginForm form){
        if(form.username == "" || form.password == "") return BadRequest();

        string hash = AuthUtils.Hash(form.password);
        Admin? admin = adminRepository.Get(a => a.Username == form.username && a.PasswordHash == hash);
        if(admin == null) return NotFound();

        JwtSecurityToken jwtOptions = new JwtSecurityToken(
            issuer: AuthUtils.ISSUER,
            audience: AuthUtils.AUDIENCE,
            claims: new List<Claim>{
                new Claim(ClaimTypes.Name, admin.Id.ToString()),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Email, admin.Email)
            },
            expires: DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
            signingCredentials: new SigningCredentials(AuthUtils.SecurityKey(), SecurityAlgorithms.HmacSha256)
        );
        return Json(new{
            accessToken=new JwtSecurityTokenHandler().WriteToken(jwtOptions)
        });
    }

    [Authorize(Policy = "Admin")]
    [HttpGet]
    public IActionResult Get(){
        string? id = HttpContext.User.Identity?.Name;
        if(id == null) return Unauthorized();

        Admin? admin = adminRepository.GetById(Int32.Parse(id));

        if(admin == null) return Forbid();
        return Json(new {
            id=admin.Id,
            username=admin.Username,
            email=admin.Email
        });
    }
}