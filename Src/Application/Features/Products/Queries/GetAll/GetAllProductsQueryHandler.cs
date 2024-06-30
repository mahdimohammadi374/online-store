using Application.Contracts;
using Application.Dto.Products;
using Application.Wrapper;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAll
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginationResponse<ProductDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var spec=new GetProductSpec(request);
            var count=await _uow.Repository<Product>().CountAsyncSpec(new ProductsCountSpec(request) , cancellationToken);
            var result = await _uow.Repository<Product>().ListAsyncSpec(spec, cancellationToken);
            var model= _mapper.Map<IEnumerable<ProductDto>>(result);
            return new PaginationResponse<ProductDto>()
            {
                Count = count,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Result = model,
            };
        }
    }
}
