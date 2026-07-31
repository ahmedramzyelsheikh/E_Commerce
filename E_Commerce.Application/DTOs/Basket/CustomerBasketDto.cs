using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.DTOs.Basket
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; } = string.Empty;

        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
    }
}
