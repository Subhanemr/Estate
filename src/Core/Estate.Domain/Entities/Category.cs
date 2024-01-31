namespace Estate.Domain.Entities
{
    public class Category : BaseNameEntity
    {
        public string Img { get; set; } = null!;
        public ICollection<Product>? Products { get; set; }

    }
}
