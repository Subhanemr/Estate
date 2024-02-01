namespace Estate.Application.ViewModels.Account
{
    public record RegisterVM(string UserName, string Name, string Surname, string Email, string Password, string ConfirmPassword);
}
