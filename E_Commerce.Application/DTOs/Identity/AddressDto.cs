using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.DTOs.Identity
{
    public class AddressDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        public string Street { get; set; } = string.Empty;
        
        [Required]
        public string City { get; set; } = string.Empty;
    }
}
