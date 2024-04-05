using GetPhone.Database.Interfaces;

namespace GetPhone.Database.Models;

public class Phone : Model {
    public new int Id {get; set;}
    public string PhoneNumber {get; set;} = "";
    public List<Review> Reviews {get; set;} = new List<Review>();
}