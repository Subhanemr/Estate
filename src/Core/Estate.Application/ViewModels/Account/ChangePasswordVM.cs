namespace Estate.Application.ViewModels
{
    public record ChangePasswordVM
    {
        public string Password { get; init; }
        public string NewPassword { get; init; }
        public string NewConfirmPassword { get; init; }

    }
}
