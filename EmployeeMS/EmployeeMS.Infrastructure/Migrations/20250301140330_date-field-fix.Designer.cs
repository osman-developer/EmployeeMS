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
    [Migration("20250301140330_date-field-fix")]
    partial class datefieldfix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmployeeMS.Domain.Entities.ContractStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ContractStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationDate = new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6576),
                            StatusName = "Active"
                        },
                        new
                        {
                            Id = 2,
                            CreationDate = new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6579),
                            StatusName = "Terminated"
                        },
                        new
                        {
                            Id = 3,
                            CreationDate = new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6581),
                            StatusName = "On Leave"
                        });
                });

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

                    b.Property<DateOnly>("HireDate")
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

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EmployeeMS.Domain.Entities.EmployeeContract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ContractStatusId")
                        .HasColumnType("int");

                    b.Property<string>("ContractTerms")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ContractTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly?>("SigningDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("ContractStatusId");

                    b.HasIndex("ContractTypeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeContracts");
                });

            modelBuilder.Entity("EmployeeMS.Domain.Entities.EmployeeContractType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContractTypeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("EmployeeContractTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContractTypeName = "Full-time",
                            CreationDate = new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6607)
                        },
                        new
                        {
                            Id = 2,
                            ContractTypeName = "Part-time",
                            CreationDate = new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6610)
                        },
                        new
                        {
                            Id = 3,
                            ContractTypeName = "Temporary ",
                            CreationDate = new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6612)
                        },
                        new
                        {
                            Id = 4,
                            ContractTypeName = "Contract",
                            CreationDate = new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6614)
                        },
                        new
                        {
                            Id = 5,
                            ContractTypeName = "Internship",
                            CreationDate = new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6616)
                        },
                        new
                        {
                            Id = 6,
                            ContractTypeName = "Freelance / Consultant",
                            CreationDate = new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6618)
                        });
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
                            CreationDate = new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6334),
                            TypeName = "profile_picture"
                        },
                        new
                        {
                            Id = 2,
                            CreationDate = new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6400),
                            TypeName = "id_picture"
                        },
                        new
                        {
                            Id = 3,
                            CreationDate = new DateTime(2025, 3, 1, 16, 3, 29, 451, DateTimeKind.Local).AddTicks(6402),
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

            modelBuilder.Entity("EmployeeMS.Domain.Entities.EmployeeContract", b =>
                {
                    b.HasOne("EmployeeMS.Domain.Entities.ContractStatus", "ContractStatus")
                        .WithMany("EmployeeContracts")
                        .HasForeignKey("ContractStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeMS.Domain.Entities.EmployeeContractType", "ContractType")
                        .WithMany("EmployeeContracts")
                        .HasForeignKey("ContractTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeMS.Domain.Entities.Employee", "Employee")
                        .WithMany("Contracts")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContractStatus");

                    b.Navigation("ContractType");

                    b.Navigation("Employee");
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

            modelBuilder.Entity("EmployeeMS.Domain.Entities.ContractStatus", b =>
                {
                    b.Navigation("EmployeeContracts");
                });

            modelBuilder.Entity("EmployeeMS.Domain.Entities.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("EmployeeMS.Domain.Entities.Employee", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("EmployeeFiles");
                });

            modelBuilder.Entity("EmployeeMS.Domain.Entities.EmployeeContractType", b =>
                {
                    b.Navigation("EmployeeContracts");
                });

            modelBuilder.Entity("EmployeeMS.Domain.Entities.EmployeeFileType", b =>
                {
                    b.Navigation("EmployeeFiles");
                });
#pragma warning restore 612, 618
        }
    }
}
