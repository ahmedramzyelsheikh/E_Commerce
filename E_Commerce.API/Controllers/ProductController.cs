using E_Commerce.API.Common;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Application.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{

    public class ProductController(IProductService productservice) : APIBaseController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDTO>>> GetAllProduct(CancellationToken  ct  )
        {
            var result = await productservice.GetAllProductsAsync(ct); 

            return ToActionResult(result);
        }

        [ProducesResponseType(typeof(ProductDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails) , StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public  async  Task<ActionResult<ProductDTO>>GetProductById(int id ,  CancellationToken ct)
        {
            var product = await productservice.GetByIdAsync(id, ct);

            return ToActionResult(product); 
        }
        [HttpGet("Brands")]
        public async  Task<ActionResult<IReadOnlyList<BrandDto>>>GetAllBrands(CancellationToken ct )
        {
            var result = await productservice.GetAllBrandsAsync(ct);
            return ToActionResult(result); 
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct )
        {
            var result = await productservice.GetAllTypesAsync(ct);
            return ToActionResult(result);
        }
          
              
    }
}
