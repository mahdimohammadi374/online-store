using Application.Contracts;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.Get
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
    {
        private readonly IUnitOfWork _uow;

        public GetProductQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken )
        {
            var spec=new GetProductSpec(request.Id);
            return await _uow.Repository<Product>().GetEntityWithSpec(spec, cancellationToken);
        }
    }
}

