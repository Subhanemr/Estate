namespace Estate.Domain.Entities
{
    public class Agency : BaseNameEntity
    {
        public ICollection<AppUser>? AppUsers { get; set; }
    }
}
