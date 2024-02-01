namespace Estate.Domain.Entities
{
    public class Favorite : BaseEntity
    {
        public string AppUserId { get; set; } = null!;
        public AppUser? AppUser { get; set; } 
        public int ProductId { get; set; }
        public Product? Product { get; set; } 
    }
}