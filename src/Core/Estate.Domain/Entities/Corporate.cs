namespace Estate.Domain.Entities
{
    public class Corporate : BaseNameEntity
    {
        public string CorporateLink { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Img { get; set; } = null!;

        public ICollection<Client>? Clients { get; set; }
    }
}
