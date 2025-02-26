﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace EmployeeMS.Domain.Entities
{
    public class Employee : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public decimal Salary { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<EmployeeFile> EmployeeFiles { get; set; }
    }
}
