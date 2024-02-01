namespace Estate.Domain.Entities
{
    public class AppUserImage : BaseEntity
    {
        public string Url { get; set; } = null!;
        public bool? IsPrimary { get; set; }
        public string AppUserId { get; set; } = null!;
        public AppUser? AppUser { get; set; }
    }
}
