using E_Commerce.Application.DTOs.Identity;
using E_Commerce.API.Common;

namespace E_Commerce.Application.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<Results<UserDto>> LoginAsync(LoginDto loginDto);
        Task<Results<UserDto>> RegisterAsync(RegisterDto registerDto);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<Results<UserDto>> GetCurrentUserAsync(string email);
        Task<Results<AddressDto>> GetUserAddressAsync(string email);
        Task<Results<AddressDto>> UpdateUserAddressAsync(string email, AddressDto addressDto);
    }
}
