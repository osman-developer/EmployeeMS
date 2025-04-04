﻿// <auto-generated />
using System;
using EmployeeMS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeMS.Infrastructure.Migrations
{
    [DbContext(typeof(EmployeeMSContext))]
    [Migration("20250226130331_departmentMigration")]
    partial class departmentMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmployeeMS.Domain.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeesNumber")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId")
                        .IsUnique()
                        .HasFilter("[ManagerId] IS NOT NULL");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("EmployeeMS.Domain.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EmployeeMS.Domain.Entities.EmployeeFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeFileTypeId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeFileTypeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeFiles");
                });

            modelBuilder.Entity("EmployeeMS.Domain.Entities.EmployeeFileType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmployeeFileTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationDate = new DateTime(2025, 2, 26, 15, 3, 31, 389, DateTimeKind.Local).AddTicks(3293),
                            TypeName = "profile_picture"
                        },
                        new
                        {
                            Id = 2,
                            CreationDate = new DateTime(2025, 2, 26, 15, 3, 31, 389, DateTimeKind.Local).AddTicks(3357),
                            TypeName = "id_picture"
                        },
                        new
                        {
                            Id = 3,
                            CreationDate = new DateTime(2025, 2, 26, 15, 3, 31, 389, DateTimeKind.Local).AddTicks(3359),
                            TypeName = "attachment"
                        });
                });

            modelBuilder.Entity("EmployeeMS.Domain.Entities.Department", b =>
                {
                    b.HasOne("EmployeeMS.Domain.Entities.Employee", "Manager")
                        .WithOne()
                        .HasForeignKey("EmployeeMS.Domain.Entities.Department", "ManagerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("EmployeeMS.Domain.Entities.Employee", b =>
                {
                    b.HasOne("EmployeeMS.Domain.Entities.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("EmployeeMS.Domain.Entities.EmployeeFile", b =>
                {
                    b.HasOne("EmployeeMS.Domain.Entities.EmployeeFileType", "EmployeeFileType")
                        .WithMany("EmployeeFiles")
                        .HasForeignKey("EmployeeFileTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeMS.Domain.Entities.Employee", "Employee")
                        .WithMany("EmployeeFiles")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("EmployeeFileType");
                });

            modelBuilder.Entity("EmployeeMS.Domain.Entities.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("EmployeeMS.Domain.Entities.Employee", b =>
                {
                    b.Navigation("EmployeeFiles");
                });

            modelBuilder.Entity("EmployeeMS.Domain.Entities.EmployeeFileType", b =>
                {
                    b.Navigation("EmployeeFiles");
                });
#pragma warning restore 612, 618
        }
    }
}
