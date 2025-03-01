using EmployeeMS.Domain.DTOs.EmployeeContract;
using EmployeeMS.Domain.DTOs.EmployeeFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.DTOs.Employee
{
    public class GetEmployeeWithoutDescDTO

    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string JobTitle { get; set; }
        public int DepartmentId { get; set; }
        public DateOnly HireDate { get; set; }
      
    }
}

