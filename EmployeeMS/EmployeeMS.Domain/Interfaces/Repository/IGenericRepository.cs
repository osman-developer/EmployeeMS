using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.Interfaces.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> Get(int id);
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate);
        Task<PagingResult<T>> GetAllPagedAsync(PagingParams<T> pagingParams, Expression<Func<T, object>>[]? includes = null);
        bool Update(T entity);
        bool Add(T entity);
        bool Delete(int id);
        bool AddRange(List<T> entities);
        bool UpdateRange(List<T> entities);
        IQueryable<T> GetQueryable();
    }
}
