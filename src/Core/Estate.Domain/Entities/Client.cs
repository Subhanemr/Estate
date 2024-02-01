namespace Estate.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string Comment { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string? Specialty { get; set; }

        public string AppUserId { get; set; } = null!;
        public int CorporateId { get; set; }
        public AppUser? AppUser { get; set; }
        public Corporate? Corporate { get; set; } 
    }
}
