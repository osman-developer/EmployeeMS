using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.Entities
{
    public class EmployeeFile : BaseEntity
    {
        public string FilePath{ get; set; }
        public string FileExtension{ get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeFileTypeId { get; set; } 
        public EmployeeFileType EmployeeFileType { get; set; }
    }
}
