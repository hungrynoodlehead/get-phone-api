using GetPhone.Database.Interfaces;
using GetPhone.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetPhone.Controllers;

[ApiController]
[Route("[controller]/{number:minlength(11)}")]
public class PhoneController : Controller {
    private readonly IRepository<Phone> phoneRepository;

    public PhoneController(IRepository<Phone> phoneRepository){
        this.phoneRepository = phoneRepository;
    }

    [HttpGet]    
    public IActionResult Get(string number){
        Phone? phone = phoneRepository.Get(p => p.PhoneNumber == number, [p => p.Reviews]);
        if(phone == null || phone.Reviews.Count == 0) return NotFound();

        List<int> reviewIds = new List<int>();
        foreach(Review r in phone.Reviews){
            reviewIds.Add(r.Id);
        }

        double Rating = phone.Reviews.Average(x => x.Rating) * 5;

        return Json(new {
            phone=phone.PhoneNumber,
            rating=Rating,
            reviews=reviewIds
        });
    }

    [HttpDelete]
    [Authorize(Policy = "Admin")]
    public IActionResult Delete(string number, IRepository<Review> reviewRepository){
        Phone? phone = phoneRepository.Get(p => p.PhoneNumber == number, [p => p.Reviews]);
        if(phone == null) return NotFound();

        foreach(Review r in phone.Reviews){
            reviewRepository.Delete(r);
        }
        phoneRepository.Delete(phone);
        phoneRepository.Save();
        return Ok();
    }
}