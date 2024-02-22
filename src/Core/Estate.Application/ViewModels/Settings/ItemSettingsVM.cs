namespace Estate.Application.ViewModels
{
    public record ItemSettingsVM
    {
        public int Id { get; init; }
        public string Key { get; init; }
        public string Value { get; init; }
    }
}
