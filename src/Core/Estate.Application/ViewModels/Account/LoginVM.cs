namespace Estate.Application.ViewModels.Account
{
    public record LoginVM(string UserNameOrEmail, string Password, bool IsRemembered);
}
