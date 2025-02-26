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
        public DbSet<Department> Departments{ get; set; }
        public DbSet<EmployeeFileType> EmployeeFileTypes { get; set; }

        public static void DataSeed(ModelBuilder modelBuilder)
        {
            //seeding EmployeeFileType Table
            modelBuilder.Entity<EmployeeFileType>().HasData(
                new EmployeeFileType { Id = 1, TypeName = Constants.EmployeeFileTypeName.profile},
                new EmployeeFileType { Id = 2, TypeName = Constants.EmployeeFileTypeName.id},
                new EmployeeFileType { Id = 3, TypeName = Constants.EmployeeFileTypeName.attachment}
              
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


