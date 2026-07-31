using E_Commerce.Domain.Models.Identity;

namespace E_Commerce.Application.Services.Contracts
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(ApplicationUser user);
    }
}
