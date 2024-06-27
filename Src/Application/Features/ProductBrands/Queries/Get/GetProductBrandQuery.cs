using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductBrands.Queries.Get
{
    public class GetProductBrandQuery : IRequest<ProductBrand>
    {
        public int Id { get; set; }

        public GetProductBrandQuery(int id)
        {
            Id = id;
        }
    }
}
