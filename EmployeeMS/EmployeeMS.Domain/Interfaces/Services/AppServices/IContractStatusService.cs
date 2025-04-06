using EmployeeMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.Interfaces.Services.AppServices
{
    public interface IContractStatusService
    {
        Task<ContractStatus> GetContractStatusByName(string statusName);
    }
}
