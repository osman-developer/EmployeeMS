using EmployeeMS.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.Interfaces.Services.HelperServices
{
    public interface IFilterBuilderService
    {
        // Apply the filter (for example, applying the search filter to PagingParams)
        void ApplySearchFilter<T>(PagingParams<T> pagingParams);
    }


}
