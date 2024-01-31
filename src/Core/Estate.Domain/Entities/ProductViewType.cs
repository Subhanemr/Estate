namespace Estate.Domain.Entities
{
    public class ProductViewType : BaseNameEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
