using EmployeeMS.Domain.Interfaces.Services.AppServices;
using EmployeeMS.Service.Services.AppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeFileController : ControllerBase
    {
        private readonly IEmployeeFileService _employeeFileService;

        public EmployeeFileController(IEmployeeFileService employeeFileService)
        {
            _employeeFileService = employeeFileService;
        }
        
        [HttpPost("delete")]
        public ActionResult<bool> DeleteEmployeeFile([FromBody] int fileId)
        {
            return Ok(_employeeFileService.Delete(fileId));
        }
    }
}
