using EmployeeMS.Domain.DTOs.Department;
using EmployeeMS.Domain.DTOs.Employee;
using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Interfaces.Services.AppServices;
using EmployeeMS.Domain.Pagination;
using EmployeeMS.Service.Services.AppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost("save")]
        public ActionResult<bool> SaveDepartment([FromBody] AddDepartmentDTO department)
        {
            if (department == null)
            {
                return BadRequest();
            }
            return Ok(_departmentService.Save(department));
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<GetDepartmentDTO>>> GetDepartments()
        {
            return Ok(await _departmentService.GetAll());
        }

        [HttpPost("all-paginated")]
        public async Task<ActionResult<PagingResult<GetDepartmentDTO>>> GetAllPagedAsync([FromBody] PagingParams<Department> paginParams)
        {
            var result = await _departmentService.GetAllPagedAsync(paginParams);
            return Ok(result);
        }
        
        [HttpPost("by-id")]
        public async Task<ActionResult<GetDepartmentDTO>> GetDepartmentById([FromBody] int departmentId)
        {
            return Ok(await _departmentService.Get(departmentId));
        }

        [HttpPost("delete")]
        public ActionResult<bool> DeleteDepartment([FromBody] int departmentId)
        {
            return Ok(_departmentService.Delete(departmentId));
        }
    }
}
