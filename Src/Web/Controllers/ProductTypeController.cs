using Application.Features.ProductBrands.Queries.Get;
using Application.Features.ProductBrands.Queries.GetAll;
using Application.Features.ProductTypes.Queries.Get;
using Application.Features.ProductTypes.Queries.GetAll;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ProductTypeController : BaseApiController
    {
        [HttpGet("get-all-product-types")]
        public async Task<IActionResult> GetAllProductTypes(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllProductTypesQuery(), cancellationToken));
        }

        [HttpGet("get-product-type/{id}")]
        public async Task<ActionResult<Product>> GetProductType([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetProductTypeQuery(id), cancellationToken));
        }
    }
}
