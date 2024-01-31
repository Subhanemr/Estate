namespace Estate.Domain.Entities
{
    public class Agency : BaseNameEntity
    {
        public ICollection<AgencyAgent>? AgencyAgents { get; set; }
    }
}
