using AutoMapper;
using EmployeeMS.Domain.DTOs.Department;
using EmployeeMS.Domain.DTOs.Employee;
using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.Interfaces.Repository;
using EmployeeMS.Domain.Interfaces.Services.AppServices;
using EmployeeMS.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Service.Services.AppServices
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IGenericRepository<Department> _departmentRepo;
        private readonly IMapper _mapper;
        public DepartmentService(IGenericRepository<Department> departmentRepo, IMapper mapper)
        {
            _departmentRepo = departmentRepo;
            _mapper = mapper;
        }

        public bool Delete(int id)
        {
            return _departmentRepo.Delete(id);
        }

        public async Task<GetDepartmentDTO> Get(int id)
        {
            var departmentResult = _departmentRepo.GetQueryable().Where(b => b.Id == id).FirstOrDefault();
            return _mapper.Map<GetDepartmentDTO>(departmentResult);
        }

        public async Task<List<GetDepartmentDTO>> GetAll()
        {
            var departmentsDB = await _departmentRepo.GetAll();
            return _mapper.Map<List<GetDepartmentDTO>>(departmentsDB);
        }

        public async Task<PagingResult<GetDepartmentDTO>> GetAllPagedAsync(PagingParams<Department> pagingParams)
        {
            var departmentResult = await _departmentRepo.GetAllPagedAsync(pagingParams);
            var departmentDTOs = _mapper.Map<List<GetDepartmentDTO>>(departmentResult.Items);

            return new PagingResult<GetDepartmentDTO>
            {
                Items = departmentDTOs,
                TotalCount = departmentResult.TotalCount,
                PageSize = departmentResult.PageSize,
                CurrentPage = departmentResult.CurrentPage
            };
        }

        public bool Save(AddDepartmentDTO department)
        {

            if (department.Id != null)
            {
                var existingDepartmentDTO = _mapper.Map<Department>(department);
                return _departmentRepo.Update(existingDepartmentDTO);

            }

            var departmentDTO = _mapper.Map<Department>(department);
            return _departmentRepo.Add(departmentDTO);

        }
    }
}
