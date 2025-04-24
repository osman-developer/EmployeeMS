using EmployeeMS.Domain.DTOs.Department;
using EmployeeMS.Domain.DTOs.EmployeeContractType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.Interfaces.Services.AppServices
{
    public interface IEmployeeContractTypeService
    {
        Task<List<GetEmployeeContractType>> GetAll();
    }
}
