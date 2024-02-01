namespace Estate.Domain.Entities
{
    public class BlogImage : BaseEntity
    {
        public byte Order { get; set; }
        public string Url { get; set; } = null!;
        public bool? IsPrimary { get; set; }
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }
    }
}
