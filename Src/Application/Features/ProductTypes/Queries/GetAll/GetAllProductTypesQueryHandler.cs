using Application.Contracts;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductTypes.Queries.GetAll
{
    public class GetAllProductTypesQueryHandler : IRequestHandler<GetAllProductTypesQuery, IReadOnlyList<ProductType>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllProductTypesQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IReadOnlyList<ProductType>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            return await _uow.Repository<ProductType>().GetAllAsync(cancellationToken);
        }
    }
}
