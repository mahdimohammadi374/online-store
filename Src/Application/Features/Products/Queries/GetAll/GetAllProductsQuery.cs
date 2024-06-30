using Application.Contracts;
using Application.Dto.Products;
using Application.Wrapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAll
{
    public class GetAllProductsQuery : RequestParametersBasics, IRequest<PaginationResponse<ProductDto>> , ICacheQuery
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public int HourseSaveData => 1;
    }
}
