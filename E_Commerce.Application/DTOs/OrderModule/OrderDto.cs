using E_Commerce.Application.DTOs.Identity;

namespace E_Commerce.Application.DTOs.OrderModule
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; } = string.Empty;
        public DateTimeOffset OrderDate { get; set; }
        public AddressDto ShipToAddress { get; set; } = new AddressDto();
        public string DeliveryMethod { get; set; } = string.Empty;
        public decimal ShippingPrice { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
