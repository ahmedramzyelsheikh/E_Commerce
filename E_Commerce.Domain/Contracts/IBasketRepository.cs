using E_Commerce.Domain.Models.Basket;

namespace E_Commerce.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? ttl = null);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
