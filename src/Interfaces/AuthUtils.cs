namespace GetPhone.Services;

interface IAuth {
    string getToken(int userId, string role);
}