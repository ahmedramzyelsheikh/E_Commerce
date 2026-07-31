using E_Commerce.Domain.Common;

namespace E_Commerce.Domain.Models.OrderModule
{
    public class OrderItem : BaseEntity<int>
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrdered itemOrdered, decimal price, int quantity)
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrdered ItemOrdered { get; set; } = new ProductItemOrdered();
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
