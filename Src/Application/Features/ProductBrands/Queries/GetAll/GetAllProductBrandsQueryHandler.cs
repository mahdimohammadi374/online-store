using Application.Contracts;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductBrands.Queries.GetAll
{
    public class GetAllProductBrandsQueryHandler : IRequestHandler<GetAllProductBrandsQuery, IReadOnlyList<ProductBrand>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllProductBrandsQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<IReadOnlyList<ProductBrand>> Handle(GetAllProductBrandsQuery request, CancellationToken cancellationToken)
        {
           return _uow.Repository<ProductBrand>().GetAllAsync(cancellationToken);
        }
    }
}
