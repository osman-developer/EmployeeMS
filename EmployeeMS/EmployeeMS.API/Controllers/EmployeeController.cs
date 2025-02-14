using EmployeeMS.Domain.DTOs.Employee;
using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Interfaces.Services.AppServices;
using EmployeeMS.Domain.Pagination;
using EmployeeMS.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
     
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("save")]
        public ActionResult<bool> SaveEmployee([FromForm] AddEmployeeDTO employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            return Ok(_employeeService.Save(employee));
        }
     
        [HttpGet("all")]
        public async Task<ActionResult<List<GetEmployeeDTO>>> GetEmployees()
        {
            return Ok(await _employeeService.GetAll());
        }
        
        [HttpPost("all-paginated")]
        public async Task<ActionResult<PagingResult<GetEmployeeDTO>>> GetAllPagedAsync([FromBody] PagingParams<Employee> paginParams)
        {
            var result = await _employeeService.GetAllPagedAsync(paginParams);
            return Ok(result);
        }

        [HttpPost("by-id")]
        public async Task<ActionResult<GetEmployeeDTO>> GetEmployeeById([FromBody] int employeeId)
        {
            return Ok(await _employeeService.Get(employeeId));
        }

        [HttpPost("delete")]
        public ActionResult<bool> DeleteEmployee([FromBody] int employeeId)
        {
            return Ok(_employeeService.Delete(employeeId));
        }
    }
}
