namespace Estate.Domain.Entities
{
    public class AgencyAppUser : BaseEntity
    {
        public int AgencyId { get; set; }
        public string AppUserId { get; set; } = null!;
        public Agency Agency { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
    }
}
