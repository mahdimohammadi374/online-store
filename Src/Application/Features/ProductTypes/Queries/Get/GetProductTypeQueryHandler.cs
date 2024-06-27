using Application.Contracts;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductTypes.Queries.Get
{
    public class GetProductTypeQueryHandler : IRequestHandler<GetProductTypeQuery, ProductType>
    {
        private readonly IUnitOfWork _uow;

        public GetProductTypeQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ProductType> Handle(GetProductTypeQuery request, CancellationToken cancellationToken)
        {
            return await _uow.Repository<ProductType>().GetByIdAsync(request.Id , cancellationToken);
        }
    }
}
