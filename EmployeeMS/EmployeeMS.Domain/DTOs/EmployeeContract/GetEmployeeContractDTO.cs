using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMS.Domain.DTOs.Employee;
using EmployeeMS.Domain.Entities;


namespace EmployeeMS.Domain.DTOs.EmployeeContract
{
    public class GetEmployeeContractDTO
    {
        public int? Id{ get; set; }
        public int EmployeeId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; } 
        public int ContractTypeId { get; set; }
        public string ContractType { get; set; }
        public decimal Salary { get; set; }
        public string Position { get; set; }
        public int ContractStatusId { get; set; }
        public string ContractStatus { get; set; }
        public DateOnly SigningDate { get; set; }
        public string ContractTerms { get; set; }
        public GetEmployeeWithoutDescDTO Employee { get; set; }

    }
}
