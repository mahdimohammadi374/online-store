using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper
{
    public abstract class RequestParametersBasics : PaginationParametersBasic
    {
        private string _search {  get; set; }
        public TypeSort TypeSort { get; set; } = TypeSort.Desc;
        public int Sort { get; set; } = 1;
        public string Search { get => _search; set =>_search= value.ToLower(); }
    }
}
public enum TypeSort
{
    Desc =1,
    asc
}