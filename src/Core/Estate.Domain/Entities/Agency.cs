namespace Estate.Domain.Entities
{
    public class Agency : BaseNameEntity
    {
        public ICollection<AgencyAppUser>? AgencyAppUsers { get; set; }
    }
}
