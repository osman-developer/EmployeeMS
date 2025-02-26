using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace EmployeeMS.Domain.Entities
{
    public class Department : BaseEntity
    {

        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Location { get; set; }
        [Required]
        public int EmployeesNumber { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public int? ManagerId { get; set; }  // Nullable, because not all departments may have a manager
        public Employee Manager { get; set; } // Navigation property to the manager (an employee)
    }
}
