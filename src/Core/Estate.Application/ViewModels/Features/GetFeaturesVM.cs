namespace Estate.Application.ViewModels
{
    public record GetFeaturesVM(int Id, string Name)
    {
        public ICollection<IncludeProductVM> Products { get; set; }
    }
}
