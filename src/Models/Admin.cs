using GetPhone.Database.Interfaces;

namespace GetPhone.Database.Models;

public class Admin : Model {
    public new int Id {get; set;}
    public string Username {get; set;} = "";
    public string PasswordHash {get; set;} = "";
    public string Email {get; set;} = "";
}