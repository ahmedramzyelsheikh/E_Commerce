using E_Commerce.Domain.Models.Basket;

namespace E_Commerce.Application.Services.Contracts
{
    public interface IPaymentService
    {
        Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);
    }
}
