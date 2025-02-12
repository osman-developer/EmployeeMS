using EmployeeMS.Domain.Interfaces.Services.HelperServices;
using EmployeeMS.Domain.Pagination;
using System.Linq.Expressions;

public class FilterBuilderService : IFilterBuilderService
{
    // Method to build the search expression for filtering
    private Expression<Func<T, bool>> BuildSearchExpression<T>(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            return null;
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        var properties = typeof(T).GetProperties()
            .Where(p => p.PropertyType == typeof(string)) // Assuming you're only searching on string properties
            .Select(p => Expression.Call(
                Expression.Property(parameter, p),
                "Contains",
                Type.EmptyTypes,
                Expression.Constant(searchString)
            ));

        var body = properties.Aggregate<Expression>((x, y) => Expression.OrElse(x, y));

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    // Method to apply the search filter to PagingParams
    public void ApplySearchFilter<T>(PagingParams<T> pagingParams)
    {
        if (!string.IsNullOrEmpty(pagingParams.SearchString))
        {
            // Build the dynamic filter expression using the search string
            pagingParams.Filter = BuildSearchExpression<T>(pagingParams.SearchString);
        }
    }
}
