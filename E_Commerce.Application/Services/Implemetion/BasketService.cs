using AutoMapper;
using E_Commerce.Application.DTOs.Basket;
using E_Commerce.Application.Services.Contracts;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Models.Basket;

using E_Commerce.API.Common;
using E_Commerce.Application.Common;

namespace E_Commerce.Application.Services.Implemetion
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<Results> DeleteBasketAsync(string basketId)
        {
            var deleted = await _basketRepository.DeleteBasketAsync(basketId);
            if (!deleted) return Results.Fail(Errors.Failure("Basket.DeleteFailed"));
            
            return Results.OK();
        }

        public async Task<Results<CustomerBasketDto>> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            
            if (basket == null)
            {
                // If basket doesn't exist, create an empty one for the client
                return Results<CustomerBasketDto>.OK(new CustomerBasketDto { Id = basketId });
            }

            return Results<CustomerBasketDto>.OK(_mapper.Map<CustomerBasketDto>(basket));
        }

        public async Task<Results<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto basketDto)
        {
            var basketToUpdate = _mapper.Map<CustomerBasket>(basketDto);
            
            var updatedBasket = await _basketRepository.UpdateBasketAsync(basketToUpdate);

            if (updatedBasket == null) return Results<CustomerBasketDto>.Fail(Errors.Failure("Basket.UpdateFailed"));

            return Results<CustomerBasketDto>.OK(_mapper.Map<CustomerBasketDto>(updatedBasket));
        }
    }
}
