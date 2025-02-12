using EmployeeMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMS.Infrastructure.Configuration
{
    public class EmployeeFileConfiguration : IEntityTypeConfiguration<EmployeeFile>
    {
        public void Configure(EntityTypeBuilder<EmployeeFile> builder)
        {
            builder.HasOne(i => i.EmployeeFileType).WithMany(p => p.EmployeeFiles).HasForeignKey(i => i.EmployeeFileTypeId).IsRequired();
            builder.HasOne(i => i.Employee).WithMany(p => p.EmployeeFiles).HasForeignKey(i => i.EmployeeId).IsRequired();
        }
    }
}
