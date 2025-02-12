using EmployeeMS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.DTOs.EmployeeFile
{
    public class AddEmployeeFileDTO
    {
       
        public int? EmployeeFileTypeId { get; set; }
        public IFormFile? File { get; set; }
    }
}
