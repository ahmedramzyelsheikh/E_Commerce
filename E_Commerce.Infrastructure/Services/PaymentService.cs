using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Models.Basket;
using E_Commerce.Domain.Models.OrderModule;
using E_Commerce.Domain.Data.Products;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace E_Commerce.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket == null) return null;

            var shippingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethodRepo = _unitOfWork.GetRepositries<DeliveryMethod, int>();
                var deliveryMethod = await deliveryMethodRepo.GetById(basket.DeliveryMethodId.Value, CancellationToken.None);
                if (deliveryMethod != null)
                {
                    shippingPrice = deliveryMethod.Cost;
                }
            }

            var productRepo = _unitOfWork.GetRepositries<E_Commerce.Domain.Data.Products.Product, int>();

            foreach (var item in basket.Items)
            {
                var productItem = await productRepo.GetById(item.Id, CancellationToken.None);
                if (productItem != null && item.Price != productItem.Price)
                {
                    item.Price = productItem.Price; // Ensure price is accurate from the database
                }
            }

            var service = new PaymentIntentService();
            PaymentIntent intent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)(shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)(shippingPrice * 100)
                };

                await service.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepository.UpdateBasketAsync(basket);

            return basket;
        }
    }
}
