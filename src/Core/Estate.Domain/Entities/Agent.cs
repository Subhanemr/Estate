namespace Estate.Domain.Entities
{
    public class Agent : BaseNameEntity
    {
        public string Surname { get; set; } = null!;

        public string PhoneFirst { get; set; } = null!;
        public string PhoneSecond { get; set; } = null!;

        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string About { get; set; } = null!;

        public string FaceLink { get; set; } = null!;
        public string TwitLink { get; set; } = null!;
        public string GoogleLink { get; set; } = null!;
        public string LinkedLink { get; set; } = null!;
        public string InstaLink { get; set; } = null!;

        public ICollection<Product>? Products { get; set; }
        public ICollection<AgencyAgent> AgencyAgents{ get; set; } = null!;
    }
}
