using EmployeeMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EmployeeMS.Infrastructure.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasMany(d => d.Employees).WithOne(e => e.Department).HasForeignKey(e => e.DepartmentId).OnDelete(DeleteBehavior.Restrict); ;
            // Foreign key in Department (nullable)
            builder.HasOne(d => d.Manager).WithOne().HasForeignKey<Department>(d => d.ManagerId).OnDelete(DeleteBehavior.SetNull).OnDelete(DeleteBehavior.SetNull);  // If the Manager is deleted, set ManagerId to null
        }
    }

}
