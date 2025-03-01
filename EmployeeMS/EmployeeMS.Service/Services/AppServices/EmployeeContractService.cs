using AutoMapper;
using EmployeeMS.Domain.DTOs.Department;
using EmployeeMS.Domain.DTOs.EmployeeContract;
using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Interfaces.Repository;
using EmployeeMS.Domain.Interfaces.Services.AppServices;
using EmployeeMS.Domain.Pagination;
using Microsoft.EntityFrameworkCore;


namespace EmployeeMS.Service.Services.AppServices
{
    public class EmployeeContractService : IEmployeeContractService
    {
        private readonly IGenericRepository<EmployeeContract> _employeeContractRepo;
        private readonly IMapper _mapper;
        public EmployeeContractService(IGenericRepository<EmployeeContract> employeeContractRepo, IMapper mapper)
        {
            _employeeContractRepo = employeeContractRepo;
            _mapper = mapper;
        }
        public bool Delete(int id)
        {
           return _employeeContractRepo.Delete(id);
        }

        public async Task<GetEmployeeContractDTO> Get(int id)
        {
            var contractResult = _employeeContractRepo.GetQueryable().Where(b => b.Id == id).Include(c => c.Employee).FirstOrDefault();
            return _mapper.Map<GetEmployeeContractDTO>(contractResult);
        }

        public async Task<List<GetEmployeeContractDTO>> GetAll()
        {
            var contractsDB = await _employeeContractRepo.GetAll();
            return _mapper.Map<List<GetEmployeeContractDTO>>(contractsDB);
        }

        public async Task<PagingResult<GetEmployeeContractDTO>> GetAllPagedAsync(PagingParams<EmployeeContract> pagingParams)
        {
            var contractsResult = await _employeeContractRepo.GetAllPagedAsync(pagingParams);
            var contractsDTOs = _mapper.Map<List<GetEmployeeContractDTO>>(contractsResult.Items);

            return new PagingResult<GetEmployeeContractDTO>
            {
                Items = contractsDTOs,
                TotalCount = contractsResult.TotalCount,
                PageSize = contractsResult.PageSize,
                CurrentPage = contractsResult.CurrentPage
            };
        }

        public bool Save(AddEmployeeContractDTO contract)
        {
            if (contract.Id != null)
            {
                var existingContractDTO = _mapper.Map<EmployeeContract>(contract);
                return _employeeContractRepo.Update(existingContractDTO);

            }

            var contractDTO = _mapper.Map<EmployeeContract>(contract);
            return _employeeContractRepo.Add(contractDTO);
        }
    }
}
