using EmployeeMS.Domain;
using EmployeeMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

using System.Reflection;

namespace EmployeeMS.Infrastructure
{
    public class EmployeeMSContext : DbContext
    {

        public EmployeeMSContext(DbContextOptions<EmployeeMSContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeFile> EmployeeFiles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeFileType> EmployeeFileTypes { get; set; }
        public DbSet<ContractStatus> ContractStatuses { get; set; }
        public DbSet<EmployeeContract> EmployeeContracts { get; set; }
        public DbSet<EmployeeContractType> EmployeeContractTypes { get; set; }

        public static void DataSeed(ModelBuilder modelBuilder)
        {
            //seeding EmployeeFileType Table
            modelBuilder.Entity<EmployeeFileType>().HasData(
                new EmployeeFileType { Id = 1, TypeName = Constants.EmployeeFileTypeName.profile },
                new EmployeeFileType { Id = 2, TypeName = Constants.EmployeeFileTypeName.id },
                new EmployeeFileType { Id = 3, TypeName = Constants.EmployeeFileTypeName.attachment }

            );
            //seeding ContractStatus Table
            modelBuilder.Entity<ContractStatus>().HasData(
                new ContractStatus { Id = 1, StatusName = Constants.ContractStatus.active },
                new ContractStatus { Id = 2, StatusName = Constants.ContractStatus.terminated },
                new ContractStatus { Id = 3, StatusName = Constants.ContractStatus.on_leave }

            );
            //seeding EmployeeContractType Table
            modelBuilder.Entity<EmployeeContractType>().HasData(
                new EmployeeContractType { Id = 1, ContractTypeName = Constants.EmployeeContractType.full_time },
                new EmployeeContractType { Id = 2, ContractTypeName = Constants.EmployeeContractType.part_time },
                new EmployeeContractType { Id = 3, ContractTypeName = Constants.EmployeeContractType.temp },
                new EmployeeContractType { Id = 4, ContractTypeName = Constants.EmployeeContractType.contract },
                new EmployeeContractType { Id = 5, ContractTypeName = Constants.EmployeeContractType.internship },
                new EmployeeContractType { Id = 6, ContractTypeName = Constants.EmployeeContractType.freelance }

            );

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            DataSeed(modelBuilder);
        }
    }


}


