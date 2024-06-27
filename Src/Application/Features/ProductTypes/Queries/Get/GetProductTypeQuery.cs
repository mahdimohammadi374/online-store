using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductTypes.Queries.Get
{
    public class GetProductTypeQuery :IRequest<ProductType>
    {
        public int Id { get; set; }

        public GetProductTypeQuery(int id)
        {
            Id = id;
        }
    }
}
