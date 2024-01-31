namespace Estate.Domain.Entities
{
    public class BlogComment : BaseEntity
    {
        public string Comment { get; set; } = null!;
        public DateTime CoomentTime { get; set; }
        public string AppUserId { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public ICollection<BlogReply>? BlogReplies { get; set; }
    }
}
