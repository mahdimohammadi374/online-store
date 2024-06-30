using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper
{
    public class PaginationParametersBasic
    {
        private const int _MaxPageSize = 50;
        private int _pageIndex { get; set; } = 1;
        private int _pageSize { get; set; } = 12;
        public int PageSize { get => _pageSize; set => _pageSize = value > _MaxPageSize ? _MaxPageSize : value; }
        public int PageIndex { get => _pageIndex; set => _pageIndex = value < 0 ? 1 : value; }
    }
}
