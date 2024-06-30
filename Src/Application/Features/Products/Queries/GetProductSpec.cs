using Application.Contracts.Specification;
using Application.Features.Products.Queries.GetAll;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries
{
    public class GetProductSpec : BaseSpecification<Product>
    {
        public GetProductSpec(GetAllProductsQuery request) : base(Expresion.ExpresionSpec(request))
        {

            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);

            if (request.TypeSort == TypeSort.Desc)
                switch (request.Sort)
                {
                    case 1:
                        AddOrderByDesc(x => x.Title);
                        break;
                    case 2:
                        AddOrderByDesc(x => x.ProductType.Title);
                        break;
                    default:
                        AddOrderByDesc(x => x.Title);
                        break;
                }
            else
                switch (request.Sort)
                {

                    case 1:
                        AddOrderBy(x => x.Title);
                        break;
                    case 2:
                        AddOrderBy(x => x.ProductType.Title);
                        break;
                    default:
                        AddOrderBy(x => x.Title);
                        break;
                }

            ApplyPagination(request.PageSize * (request.PageIndex - 1), request.PageSize, true);
        }

        
        public GetProductSpec(int id) : base(x => x.Id == id)
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
        }


    }
}

public class ProductsCountSpec : BaseSpecification<Product>
{
    public ProductsCountSpec(GetAllProductsQuery request) : base(Expresion.ExpresionSpec(request))
    {
        IsPagingEnabled = false;
    }
}


public static class Expresion
{
    public static Expression<Func<Product, bool>> ExpresionSpec(GetAllProductsQuery request)
    {
        return x =>
                (string.IsNullOrEmpty(request.Search) || x.Title.ToLower().Contains(request.Search)) &&
                (!request.BrandId.HasValue || x.ProductBrandId == request.BrandId) &&
                (!request.TypeId.HasValue || x.ProductTypeId == request.TypeId);
    }

}