using AutoMapper;
using EmployeeMS.Domain.DTOs.Employee;
using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Interfaces.Repository;
using EmployeeMS.Domain.Interfaces.Services.AppServices;
using EmployeeMS.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMS.Service.Services.AppServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _repo;
        private readonly IEmployeeFileService _employeeFileService;
        private readonly IMapper _mapper;
        public EmployeeService(IGenericRepository<Employee> repo, IEmployeeFileService employeeFileService, IMapper mapper)
        {
            _repo = repo;
            _employeeFileService = employeeFileService;
            _mapper = mapper;
        }
        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        public async Task<GetEmployeeDTO> Get(int id)
        {
            var employeeResult = _repo.GetQueryable().Where(b => b.Id == id).Include(emp => emp.EmployeeFiles).FirstOrDefault();
            return _mapper.Map<GetEmployeeDTO>(employeeResult);
        }

        public async Task<List<GetEmployeeDTO>> GetAll()
        {
            var employeesDB = await _repo.GetAll();
            return _mapper.Map<List<GetEmployeeDTO>>(employeesDB);

        }
        public async Task<PagingResult<GetEmployeeDTO>> GetAllPagedAsync(PagingParams<Employee> pagingParams)
        {
            var employeeResult = await _repo.GetAllPagedAsync(pagingParams);
            var employeeDTOs = _mapper.Map<List<GetEmployeeDTO>>(employeeResult.Items);

            return new PagingResult<GetEmployeeDTO>
            {
                Items = employeeDTOs,
                TotalCount = employeeResult.TotalCount,
                PageSize = employeeResult.PageSize,
                CurrentPage = employeeResult.CurrentPage
            };
        }

        public bool Save(AddEmployeeDTO employee)
        {
            if (employee.Id != null)
            {
                var existingEmployeeDTO = _mapper.Map<Employee>(employee);
                var updatedEmp = _repo.Update(existingEmployeeDTO);
                _employeeFileService.Save(employee.EmployeeFiles, existingEmployeeDTO.Id);
                return updatedEmp;
            }

            var employeeDTO = _mapper.Map<Employee>(employee);
            var emp = _repo.Add(employeeDTO);
            //add file
            _employeeFileService.Save(employee.EmployeeFiles, employeeDTO.Id);
            return emp;
        }
    }
}
