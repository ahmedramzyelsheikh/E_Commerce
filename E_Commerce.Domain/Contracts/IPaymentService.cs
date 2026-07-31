using E_Commerce.Domain.Models.Basket;

namespace E_Commerce.Domain.Contracts
{
    public interface IPaymentService
    {
        Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);
    }
}
