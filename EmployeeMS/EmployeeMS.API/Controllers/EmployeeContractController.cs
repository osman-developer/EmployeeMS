using EmployeeMS.Domain.DTOs.Department;
using EmployeeMS.Domain.DTOs.EmployeeContract;
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
    public class EmployeeContractController : ControllerBase
    {
        private readonly IEmployeeContractService _employeeContractService;

        public EmployeeContractController(IEmployeeContractService employeeContractService)
        {
            _employeeContractService = employeeContractService;
        }

        [HttpPost("save")]
        public ActionResult<bool> SaveContract([FromBody] AddEmployeeContractDTO contract)
        {
            if (contract == null)
            {
                return BadRequest();
            }
            return Ok(_employeeContractService.Save(contract));
        }
        
        [HttpGet("all")]
        public async Task<ActionResult<List<GetEmployeeContractDTO>>> GetEmployeesContracts()
        {
            return Ok(await _employeeContractService.GetAll());
        }
        
        [HttpPost("all-paginated")]
        public async Task<ActionResult<PagingResult<GetEmployeeContractDTO>>> GetAllPagedAsync([FromBody] PagingParams<EmployeeContract> paginParams)
        {
            var result = await _employeeContractService.GetAllPagedAsync(paginParams);
            return Ok(result);
        }
        
        [HttpPost("by-id")]
        public async Task<ActionResult<GetEmployeeContractDTO>> GetEmployeeContractById([FromBody] int contractId)
        {
            return Ok(await _employeeContractService.Get(contractId));
        }

        [HttpPost("delete")]
        public ActionResult<bool> DeleteDepartment([FromBody] int contractId)
        {
            return Ok(_employeeContractService.Delete(contractId));
        }

        [HttpPost("generate-report")]
        public async Task<IActionResult> GenerateEmployeeContractPdfReport([FromBody] int contractId)
        {
            try
            {
                // Call the service to generate the PDF byte array and get the sanitized contract name
                var (pdfBytes, sanitizedFileName) = await _employeeContractService.GenerateEmployeeContractPdfReportWithTemplateAsync(contractId);

                // Set the content disposition header to ensure the browser knows it's an attachment
                Response.Headers["Content-Disposition"] = $"attachment; filename={sanitizedFileName}.pdf";

                // Return the PDF file as a response with the sanitized file name
                return File(pdfBytes, "application/pdf");
            }
            catch (FileNotFoundException ex)
            {
                return NotFound($"Template not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}