using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.Entities
{
    public class EmployeeContractType :BaseEntity
    {

        [Required]
        [MaxLength(100)]
        public string ContractTypeName { get; set; }
        public ICollection<EmployeeContract> EmployeeContracts { get; set; }
    }
}
