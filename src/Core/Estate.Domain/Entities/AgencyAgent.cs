namespace Estate.Domain.Entities
{
    public class AgencyAgent : BaseEntity
    {
        public int AgencyId { get; set; }
        public int AgentId { get; set; }
        public Agency Agency { get; set; } = null!;
        public Agent Agent { get; set; } = null!;
    }
}
