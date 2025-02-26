using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMS.Domain.Entities;
namespace EmployeeMS.Domain.DTOs.Department
{
    public class GetDepartmentDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int EmployeesNumber { get; set; }
        public int? ManagerId { get; set; }  
    }
}
