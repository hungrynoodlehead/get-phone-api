using GetPhone.Database;
using GetPhone.Database.Models;
using GetPhone.Database.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]/[action]")]
public class AdminController : Controller {
    private readonly IRepository<Admin> adminRepository;
    public AdminController(IRepository<Admin> adminRepository){
        this.adminRepository = adminRepository;
    }
    [HttpGet]
    public IActionResult Get(){
        return Ok(adminRepository.GetById(1));
    }
}