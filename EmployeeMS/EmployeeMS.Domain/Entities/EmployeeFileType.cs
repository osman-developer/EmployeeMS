using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeMS.Domain.Entities
{
    public class EmployeeFileType :BaseEntity
    {
        public string TypeName { get; set; }
        public ICollection<EmployeeFile> EmployeeFiles { get; set; }
    }
}
