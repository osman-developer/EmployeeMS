using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Interfaces.Repository;
using EmployeeMS.Domain.Interfaces.Services.HelperServices;
using EmployeeMS.Domain.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace EmployeeMS.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly EmployeeMSContext _context;
        private readonly IFilterBuilderService _filterBuilderService;
        public GenericRepository(EmployeeMSContext context, IFilterBuilderService filterBuilderService)
        {
            _context = context;
            _filterBuilderService = filterBuilderService;
        }
        public bool Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges() > 0;
        }

        public bool AddRange(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return _context.SaveChanges() > 0;
        }
        public bool UpdateRange(List<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            return _context.SaveChanges() > 0;
        }
        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public bool Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges() > 0;
        }
        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>().AsNoTracking().AsQueryable();
        }

        public async Task<PagingResult<T>> GetAllPagedAsync(PagingParams<T> pagingParams, Expression<Func<T, object>>[]? includes = null)
        {
            // Apply the dynamic filter using the FilterBuilderService
            _filterBuilderService.ApplySearchFilter(pagingParams);
            
            var query = _context.Set<T>().AsQueryable();

            // Apply dynamic includes if any
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            // Apply the filter if it was set by FilterBuilderService
            if (pagingParams.Filter != null)
            {
                query = query.Where(pagingParams.Filter);
            }

            // Get total count for pagination
            var totalCount = await query.CountAsync();

            // Apply pagination
            var items = await query
                .Skip((pagingParams.PageIndex - 1) * pagingParams.PageSize)
                .Take(pagingParams.PageSize)
                .ToListAsync();

            return new PagingResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = pagingParams.PageSize,
                CurrentPage = pagingParams.PageIndex
            };
        }

    }
}
