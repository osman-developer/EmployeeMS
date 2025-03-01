using EmployeeMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace EmployeeMS.Infrastructure.Configuration
{
    public class EmployeeContractConfiguration : IEntityTypeConfiguration<EmployeeContract>
    {
        public void Configure(EntityTypeBuilder<EmployeeContract> builder)
        {
            builder
           .HasOne(ec => ec.Employee)       // Each contract has one employee
           .WithMany(e => e.Contracts)      // An employee can have many contracts
           .HasForeignKey(ec => ec.EmployeeId) // Foreign key to Employee
           .OnDelete(DeleteBehavior.Cascade); // If an employee is deleted, their contracts will be deleted

            builder
          .HasOne(e => e.ContractType)  // An EmployeeContract has one EmployeeContractType
          .WithMany(c => c.EmployeeContracts)  // An EmployeeContractType can have many EmployeeContracts
          .HasForeignKey(e => e.ContractTypeId)  // Foreign key in EmployeeContract to EmployeeContractType
          .OnDelete(DeleteBehavior.Cascade);

            builder
           .HasOne(ec => ec.ContractStatus) // Each contract has one status
           .WithMany(cs => cs.EmployeeContracts) // A status can have many contracts
           .HasForeignKey(ec => ec.ContractStatusId); // Foreign key to ContractStatus


        }
    }
}
