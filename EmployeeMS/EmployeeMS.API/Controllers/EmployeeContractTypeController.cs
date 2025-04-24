using EmployeeMS.Domain.DTOs.Employee;
using EmployeeMS.Domain.DTOs.EmployeeContractType;
using EmployeeMS.Domain.Interfaces.Services.AppServices;
using EmployeeMS.Service.Services.AppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeContractTypeController : ControllerBase
    {
        private readonly IEmployeeContractTypeService _employeeContractTypeService;

        public EmployeeContractTypeController(IEmployeeContractTypeService employeeContractTypeService)
        {
            _employeeContractTypeService = employeeContractTypeService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<GetEmployeeContractType>>> GetEmployeeContractTypes()
        {
            return Ok(await _employeeContractTypeService.GetAll());
        }
    }
}
