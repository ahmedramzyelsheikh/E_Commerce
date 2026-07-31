namespace E_Commerce.Domain.Models.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; } = string.Empty;
        public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
        public string PaymentIntentId { get; set; } = string.Empty;

        public CustomerBasket()
        {
        }

        public CustomerBasket(string id)
        {
            Id = id;
        }
    }
}
