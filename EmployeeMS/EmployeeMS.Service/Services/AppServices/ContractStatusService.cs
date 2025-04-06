using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Interfaces.Repository;
using EmployeeMS.Domain.Interfaces.Services.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Service.Services.AppServices
{
    public class ContractStatusService : IContractStatusService
    {
        private readonly IGenericRepository<ContractStatus> _contractStatusRepo;
        public ContractStatusService(IGenericRepository<ContractStatus> contractStatusRepo)
        {
            _contractStatusRepo = contractStatusRepo;
        }
        public async Task<ContractStatus> GetContractStatusByName(string statusName)
        {
            var contractStatuses = _contractStatusRepo.GetQueryable().Where(cs => cs.StatusName == statusName);
            return contractStatuses.FirstOrDefault(); // Safe to use FirstOrDefault after await
        }

    }
}
