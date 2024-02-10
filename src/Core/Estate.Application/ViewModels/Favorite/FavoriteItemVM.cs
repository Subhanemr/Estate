namespace Estate.Application.ViewModels
{
    public record FavoriteItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string OrderDayOrMoth { get; set; }
        public string Description { get; set; }
        public float Area { get; set; }
        public byte Bedrooms { get; set; }
        public byte Bathrooms { get; set; }
        public byte Garages { get; set; }
    }
}
