using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.Entities
{
    public class ContractStatus :BaseEntity
    {
        public string StatusName { get; set; }    

        public ICollection<EmployeeContract> EmployeeContracts { get; set; } // A status can be associated with many contracts
    }
}
