namespace Estate.Domain.Entities
{
    public class Client : BaseNameEntity
    {
        public string Surname { get; set; } = null!;
        public string? Specialty { get; set; }
        public string Country { get; set; } = null!;
        public string Comment { get; set; } = null!;

        public int CorporateId { get; set; }
        public Corporate Corporate { get; set; } = null!;
    }
}
