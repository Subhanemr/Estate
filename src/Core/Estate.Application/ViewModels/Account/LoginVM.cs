namespace Estate.Application.ViewModels
{
    public record LoginVM(string UserNameOrEmail, string Password, bool IsRemembered);
}
