using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Domain.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = string.Empty;
        
        // Navigation Property for 1-1 relationship with Address
        public Address Address { get; set; }
    }
}
