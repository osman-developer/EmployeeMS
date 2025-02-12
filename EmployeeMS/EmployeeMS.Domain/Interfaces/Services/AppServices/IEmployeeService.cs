using EmployeeMS.Domain.DTOs.Employee;
using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.Interfaces.Services.AppServices
{
    public interface IEmployeeService
    {
        bool Save(AddEmployeeDTO employee);
        bool Delete(int id);
        Task<List<GetEmployeeDTO>> GetAll();
        Task<PagingResult<GetEmployeeDTO>> GetAllPagedAsync(PagingParams<Employee> pagingParams);
        Task<GetEmployeeDTO> Get(int id);
    }
}
