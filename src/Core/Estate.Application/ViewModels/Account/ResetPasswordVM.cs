namespace Estate.Application.ViewModels
{
    public record ResetPasswordVM
    {
        public string NewPassword { get; init; }
        public string NewConfirmPassword { get; init; }

    }
}
