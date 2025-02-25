using EmployeeMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.DTOs.EmployeeFile
{
    public class GetEmployeeFileDTO
    {
        public int? Id { get; set; }
        public string FilePath { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeFileTypeId { get; set; }
    }
}
