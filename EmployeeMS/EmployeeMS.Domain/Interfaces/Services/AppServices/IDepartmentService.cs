using EmployeeMS.Domain.DTOs.Department;
using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.Interfaces.Services.AppServices
{
    public interface IDepartmentService
    {
        bool Save(AddDepartmentDTO department);
        bool Delete(int id);
        Task<List<GetDepartmentDTO>> GetAll();
        Task<PagingResult<GetDepartmentDTO>> GetAllPagedAsync(PagingParams<Department> pagingParams);
        Task<GetDepartmentDTO> Get(int id);
    }
}
