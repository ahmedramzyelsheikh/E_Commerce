using System.ComponentModel.DataAnnotations;
using E_Commerce.Application.DTOs.Identity;

namespace E_Commerce.Application.DTOs.OrderModule
{
    public class CreateOrderDto
    {
        [Required]
        public string BasketId { get; set; } = string.Empty;

        [Required]
        public int DeliveryMethodId { get; set; }

        [Required]
        public AddressDto ShipToAddress { get; set; } = new AddressDto();
    }
}
