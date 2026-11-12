namespace E_Commerce.Domain.Models.Basket
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
