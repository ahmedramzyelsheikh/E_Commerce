using E_Commerce.Application.Services.Contracts;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Models.Products;
using E_Commerce.Domain.Models.OrderModule;
using E_Commerce.Application.Specification;

namespace E_Commerce.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order?> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // 1. Get basket from the repo
            var basket = await _basketRepo.GetBasketAsync(basketId);
            if (basket == null) return null;

            // 2. Get items from the product repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.GetRepository<Product, int>().GetById(item.Id, CancellationToken.None);
                if (productItem is null) return null; // basket has a product that no longer exists

                // Price always comes from the database, never from the basket
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }
            if (items.Count == 0) return null;

            // 3. Get delivery method from repo
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetById(deliveryMethodId, CancellationToken.None);
            if (deliveryMethod is null) return null;

            // 4. Calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            // 5. Create order (the payment intent is only set after POST /api/Payment/{basketId})
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, items, subtotal, basket.PaymentIntentId ?? string.Empty);

            _unitOfWork.GetRepository<Order, int>().Add(order);

            // 6. Save to db
            var result = await _unitOfWork.SaveChangeAsynce(CancellationToken.None);

            if (result <= 0) return null;

            // 7. Delete basket
            await _basketRepo.DeleteBasketAsync(basketId);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var result = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAll(CancellationToken.None);
            return result.ToList();
        }

        public async Task<Order?> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrderWithItemsAndOrderingSpecification(id, buyerEmail);
            return await _unitOfWork.GetRepository<Order, int>().GetById(spec, CancellationToken.None);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemsAndOrderingSpecification(buyerEmail);
            var result = await _unitOfWork.GetRepository<Order, int>().GetAll(spec, CancellationToken.None);
            return result.ToList();
        }
    }
}
