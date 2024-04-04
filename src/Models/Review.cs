using GetPhone.Database.Interfaces;

namespace GetPhone.Database.Models;

public class Review : Model {
    public new int Id {get; set;}
    public Phone Phone {get; set;} = new Phone();
    public int Rating {get; set;}
    public string AuthorIP {get; set;} = "";
    public string AuthorName {get; set;} = "";
    public string Comment {get; set;} = "";
    public bool Scam {get; set;} = false;
    public bool Threats {get; set;} = false;
    public bool Message {get; set;} = false;
    public bool AutomaticCall {get; set;} = false;
    public bool CallCenter {get; set;} = false;
    public bool Advertisement {get; set;} = false;
    public bool Polls {get; set;} = false;
    public bool FinancialServices {get; set;} = false;
}