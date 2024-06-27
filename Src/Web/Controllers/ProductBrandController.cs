using Application.Features.ProductBrands.Queries.Get;
using Application.Features.ProductBrands.Queries.GetAll;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ProductBrandController : BaseApiController
    {
        [HttpGet("get-all-product-brands")]
        public async Task<IActionResult> GetAllProductBrands(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllProductBrandsQuery(), cancellationToken));
        }

        [HttpGet("get-product-brand/{id}")]
        public async Task<ActionResult<Product>> GetProductBrand([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetProductBrandQuery(id), cancellationToken));
        }
    }
}
