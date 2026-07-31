using System.Text.Json;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Models.Basket;
using StackExchange.Redis;

namespace E_Commerce.Infrastructure.Repositries
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);
            
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data!);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? ttl = null)
        {
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), ttl ?? TimeSpan.FromDays(30));
            
            if (!created) return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}
