using GetPhone.Database;
using GetPhone.Database.Interfaces;
using GetPhone.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ReviewController : Controller {
    private readonly IRepository<Review> reviewRepository;
    private readonly IRepository<Phone> phoneRepository;

    public ReviewController(IRepository<Review> reviewRepository, IRepository<Phone> phoneRepository){
        this.reviewRepository = reviewRepository;
        this.phoneRepository = phoneRepository;
    }

    public class ReviewForm {
        public int rating {get; set;}
        public string author_name {get; set;} = "";
        public string comment {get; set;} = "";
        public bool scam {get; set;} = false;
        public bool threats {get; set;} = false;
        public bool message {get; set;} = false;
        public bool automatic_call {get; set;} = false;
        public bool call_center {get; set;} = false;
        public bool advertisement {get; set;} = false;
        public bool polls {get; set;} = false;
        public bool financial_services {get; set;} = false;
    }

    [HttpGet]
    [Route("phone/{number:minlength(11)}/{id:int}")]
    public IActionResult Get(int id){
        Review? review = reviewRepository.Get(r => r.Id == id);
        if(review == null) return NotFound();
        return Json(new {
            id=review.Id,
            rating=review.Rating,
            author_name=review.AuthorName,
            comment=review.Comment,
            scam=review.Scam,
            threats=review.Threats,
            message=review.Message,
            automatic_call=review.AutomaticCall,
            call_center=review.CallCenter,
            advertisement=review.Advertisement,
            polls=review.Polls,
            financial_services=review.FinancialServices
        });
    }

    [HttpPost]
    [Route("phone/{number:minlength(11)}/create")]
    public IActionResult Post(string number, ReviewForm form){
        Phone? phone = phoneRepository.Get(p => p.PhoneNumber == number);
        if(phone == null){
            phone = new Phone();
            phone.PhoneNumber = number;
            phoneRepository.Add(phone);
            phoneRepository.Save();
        }

        Review review = new Review();
        if(form.rating < 0 && form.rating > 1) return BadRequest();
        if(form.comment == "") return BadRequest();
        if(form.author_name == "") return BadRequest();

        review.Phone = phone;
        review.Rating = form.rating;
        review.AuthorName = form.author_name;
        review.Comment = form.comment;
        review.Scam = form.scam;
        review.Threats = form.threats;
        review.Message = form.message;
        review.AutomaticCall = form.automatic_call;
        review.CallCenter = form.call_center;
        review.Advertisement = form.advertisement;
        review.Polls = form.polls;
        review.FinancialServices = form.financial_services;
        
        reviewRepository.Add(review);
        reviewRepository.Save();
        return Json(new {
            id=review.Id
        });
    }

    [HttpPut]
    [Route("phone/{number:minlength(11)}/{id:int}")]
    [Authorize(Policy = "Admin")]
    public IActionResult Put(string number, ReviewForm form){
        Review? review = reviewRepository.Get(r => r.Phone.PhoneNumber == number);

        if(review == null) return NotFound();
        if(form.rating < 0 && form.rating > 1) return BadRequest();
        if(form.comment == "") return BadRequest();
        if(form.author_name == "") return BadRequest();
        review.Rating = form.rating;
        review.AuthorName = form.author_name;
        review.Comment = form.comment;
        review.Scam = form.scam;
        review.Threats = form.threats;
        review.Message = form.message;
        review.AutomaticCall = form.automatic_call;
        review.CallCenter = form.call_center;
        review.Advertisement = form.advertisement;
        review.Polls = form.polls;
        review.FinancialServices = form.financial_services;
        reviewRepository.Update(review);
        reviewRepository.Save();
        return Ok();
    }

    [HttpDelete]
    [Route("phone/{number:minlength(11)}/{id:int}")]
    [Authorize(Policy = "Admin")]
    public IActionResult Delete(string number){
        Review? review = reviewRepository.Get(r => r.Phone.PhoneNumber == number);
        if(review == null) return NotFound();

        reviewRepository.Delete(review);
        reviewRepository.Save();

        return Ok();
    }
}