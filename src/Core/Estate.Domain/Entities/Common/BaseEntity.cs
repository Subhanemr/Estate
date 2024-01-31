namespace Estate.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string CreatedBy { get; set; } = null!;

        public BaseEntity()
        {
            CreatedBy = "Doggy";
        }
    }
}
