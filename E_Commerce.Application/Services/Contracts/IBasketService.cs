using E_Commerce.Application.DTOs.Basket;

using E_Commerce.API.Common;

namespace E_Commerce.Application.Services.Contracts
{
    public interface IBasketService
    {
        Task<Results<CustomerBasketDto>> GetBasketAsync(string basketId);
        Task<Results<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto basketDto);
        Task<Results> DeleteBasketAsync(string basketId);
    }
}
