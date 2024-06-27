using Application.Contracts.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries
{
    public class GetProductSpec : BaseSpecification<Product>
    {
        public GetProductSpec() :base()
        {
            AddIncludes(x=>x.ProductType);
            AddIncludes(x=>x.ProductBrand);
        }

        public GetProductSpec(int id) : base(x=>x.Id==id)
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
        }


    }
}
