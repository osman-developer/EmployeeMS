using EmployeeMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeMS.Infrastructure.Configuration
{
    public class EmployeeFileTypeConfiguration : IEntityTypeConfiguration<EmployeeFileType>
    {
        public void Configure(EntityTypeBuilder<EmployeeFileType> builder)
        {
            builder.HasMany(i => i.EmployeeFiles).WithOne(p => p.EmployeeFileType).IsRequired();

        }
    }
}
