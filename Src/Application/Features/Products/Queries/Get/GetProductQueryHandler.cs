using Application.Contracts;
using Application.Dto.Products;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.Get
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken )
        {
            var spec=new GetProductSpec(request.Id);
            var result = await _uow.Repository<Product>().GetEntityWithSpec(spec, cancellationToken);

            return _mapper.Map<ProductDto>(result);
        }
    }
}

