using EmployeeMS.Domain.DTOs.EmployeeFile;
using EmployeeMS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.Interfaces.Services.AppServices
{
    public interface IEmployeeFileService
    {
        bool Delete(int employeeFileId);
        bool Save(ICollection<AddEmployeeFileDTO> EmployeeFiles, int employeeId);
    }
}
