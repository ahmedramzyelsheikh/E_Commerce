using E_Commerce.Application.DTOs.Basket;
using E_Commerce.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class BasketsController : APIBaseController
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasket(string id)
        {
            var result = await _basketService.GetBasketAsync(id);
            return ToActionResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basket)
        {
            var result = await _basketService.UpdateBasketAsync(basket);
            return ToActionResult(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBasket(string id)
        {
            var result = await _basketService.DeleteBasketAsync(id);
            return ToActionResult(result);
        }
    }
}
