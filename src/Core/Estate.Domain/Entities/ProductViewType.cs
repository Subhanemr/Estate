namespace Estate.Domain.Entities
{
    public class ProductViewType : BaseEntity
    {
        public int ProductId { get; set; }
        public int ViewTypeId { get; set; }
        public Product? Product { get; set; } 
        public ViewType? ViewType { get; set; } 
    }
}
