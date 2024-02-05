namespace Estate.Domain.Entities
{
    public class BlogComment : BaseEntity
    {
        public string Comment { get; set; } = null!;
        public string AppUserId { get; set; } = null!;
        public AppUser? AppUser { get; set; }
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }
        public ICollection<BlogReply>? BlogReplies { get; set; }
    }
}
