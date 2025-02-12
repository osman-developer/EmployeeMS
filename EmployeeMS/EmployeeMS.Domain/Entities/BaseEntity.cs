using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Domain.Entities
{
    public class BaseEntity
    {
        //[Required]
        public int Id { get; set; }
        //[Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
