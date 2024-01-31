namespace Estate.Domain.Entities
{
    public abstract class BaseNameEntity : BaseEntity
    {
        public string Name { get; set; } = null!;

    }
}
