namespace Estate.Domain.Entities
{
    public class ViewType :BaseNameEntity
    {
        public ICollection<ProductViewType>? ProductViewTypes { get; set; }
    }
}
