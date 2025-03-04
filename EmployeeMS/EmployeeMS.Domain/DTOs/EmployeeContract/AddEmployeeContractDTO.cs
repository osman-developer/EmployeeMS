using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.DTOs.EmployeeContract
{
    public class AddEmployeeContractDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; } // Nullable if it's an indefinite contract
        public int ContractTypeId { get; set; }
        public decimal Salary { get; set; }
        public string Position { get; set; }
        public int ContractStatusId { get; set; }
        public DateOnly SigningDate { get; set; }
        public string ContractTerms { get; set; }

    }
}
