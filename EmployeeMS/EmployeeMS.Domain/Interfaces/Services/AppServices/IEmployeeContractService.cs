using EmployeeMS.Domain.DTOs.Department;
using EmployeeMS.Domain.DTOs.EmployeeContract;
using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.Interfaces.Services.AppServices
{
    public interface IEmployeeContractService
    {
        bool Save(AddEmployeeContractDTO contract);
        bool Delete(int id);
        Task<List<GetEmployeeContractDTO>> GetAll();
        Task<PagingResult<GetEmployeeContractDTO>> GetAllPagedAsync(PagingParams<EmployeeContract> pagingParams);
        Task<GetEmployeeContractDTO> Get(int id);
        Task<(byte[] pdfBytes, string sanitizedFileName)> GenerateEmployeeContractPdfReportWithTemplateAsync(int contractId);
    }
}
