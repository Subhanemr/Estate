namespace Estate.Domain.Entities
{
    public class AgentImage : BaseEntity
    {
        public string Url { get; set; } = null!;
        public bool? IsPrimary { get; set; }
        public int AgentId { get; set; }
        public Agent Agent { get; set; } = null!;
    }
}
