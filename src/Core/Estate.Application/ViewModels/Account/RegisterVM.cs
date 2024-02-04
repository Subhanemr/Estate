namespace Estate.Application.ViewModels
{
    public record RegisterVM(string UserName, string Name, string Surname, string? Email, string? Phone,string Password, string ConfirmPassword);
}
