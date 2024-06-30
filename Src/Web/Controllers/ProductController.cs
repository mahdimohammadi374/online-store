using Application.Features.ProductBrands.Queries.Get;
using Application.Features.ProductBrands.Queries.GetAll;
using Application.Features.Products.Queries.Get;
using Application.Features.Products.Queries.GetAll;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ProductController : BaseApiController
    {
        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet("get-product/{id}")]
        public async Task<ActionResult<Product>> GetProduct([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetProductQuery(id), cancellationToken));
        }

      
    }
}
