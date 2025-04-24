using AutoMapper;
using EmployeeMS.Domain.DTOs.EmployeeContract;
using EmployeeMS.Domain.DTOs.EmployeeContractType;
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
    public class EmployeeContractTypeService : IEmployeeContractTypeService
    {
        private readonly IGenericRepository<EmployeeContractType> _contractTypeRepo;
        private readonly IMapper _mapper;
        public EmployeeContractTypeService(IGenericRepository<EmployeeContractType> contractTypeRepo, IMapper mapper)
        {
            _contractTypeRepo = contractTypeRepo;
            _mapper = mapper;
        }
        public async Task<List<GetEmployeeContractType>> GetAll()
        {
            var contractTypesDB = await _contractTypeRepo.GetAll();
            return _mapper.Map<List<GetEmployeeContractType>>(contractTypesDB);
        }
    }
}
