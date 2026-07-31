using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.DTOs.Basket
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public string PictureUrl { get; set; } = string.Empty;
        
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least one")]
        public int Quantity { get; set; }
    }
}
