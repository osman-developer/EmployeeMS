using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.Pagination
{
    public class PagingParams<T>
    {
        public int PageIndex { get; set; } = 1; // Default to first page
        public int PageSize { get; set; } = 10; // Default to 10 items per page
        public string? SearchString { get; set; }
        public Expression<Func<T, bool>>? Filter { get; set; } // Optional filter expression
    }

}
