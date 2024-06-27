using Application.Contracts;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductBrands.Queries.Get
{
    public class GetProductBrandQueryHandler : IRequestHandler<GetProductBrandQuery, ProductBrand>
    {
        private readonly IUnitOfWork _uow;

        public GetProductBrandQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<ProductBrand> Handle(Get.GetProductBrandQuery request, CancellationToken cancellationToken)
        {
            return _uow.Repository<ProductBrand>().GetByIdAsync(request.Id, cancellationToken); 
        }
    }
}
