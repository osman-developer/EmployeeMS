using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.Entities
{
    public class EmployeeContract : BaseEntity
    {
        public int EmployeeId { get; set; }
        [Required]
        public string Name{ get; set; }
        [Required]
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; } // Nullable if it's an indefinite contract
        [Required]
        public int ContractTypeId { get; set; } 
        public EmployeeContractType ContractType { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public string Position { get; set; }
        public int ContractStatusId { get; set; }
        public ContractStatus ContractStatus { get; set; } 
        public DateOnly? SigningDate { get; set; }
        public string ContractTerms { get; set; }
        public Employee Employee { get; set; }
    }
}