using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;
using EmployeeMS.Domain.DTOs.Employee;
using EmployeeMS.Domain.Entities;
using EmployeeMS.Domain.DTOs.EmployeeFile;
using System.Xml.Linq;
using EmployeeMS.Domain.DTOs.Department;
namespace EmployeeMS.Domain.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            //create map for the employee and employeeDto
            CreateMap<Employee, GetEmployeeDTO>()
                 .ForMember(dest => dest.EmployeeFiles, opt => opt.MapFrom(src => src.EmployeeFiles));

            //create map between employeefile and EmployeefileDto with ignoring the other fields
            CreateMap<EmployeeFile, GetEmployeeFileDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.EmployeeFileTypeId, opt => opt.MapFrom(src => src.EmployeeFileTypeId))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<AddEmployeeDTO, Employee>().ForMember(emp => emp.EmployeeFiles, opt => opt.Ignore());
            
            CreateMap<Department, GetDepartmentDTO>();
            CreateMap<AddDepartmentDTO, Department>().ForMember(dep => dep.Employees, opt => opt.Ignore())
                .ForMember(dep => dep.Employees, opt => opt.Ignore());

        }
    }
}