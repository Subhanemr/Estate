namespace Estate.Domain.Entities
{
    public class AppUserImage : BaseEntity
    {
        public string Url { get; set; } = null!;
        public bool? IsPrimary { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
    }
}
