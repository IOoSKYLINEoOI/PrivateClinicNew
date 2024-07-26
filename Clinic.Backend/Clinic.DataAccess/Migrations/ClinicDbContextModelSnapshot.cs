﻿// <auto-generated />
using System;
using Clinic.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Clinic.DataAccess.Migrations
{
    [DbContext(typeof(ClinicDbContext))]
    partial class ClinicDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Clinic.DataAccess.Models.AddressEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ApartmentNumber")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<string>("Pavilion")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.DepartmentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.EmployeeDepartmentEntity", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<Guid>("PositionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EmployeeId", "DepartmentId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PositionId");

                    b.ToTable("EmployeeDepartments");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.EmployeeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly?>("DateOfDismissal")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateOnly>("HiringDate")
                        .HasColumnType("date");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.ImageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.PermissionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ReadResult"
                        },
                        new
                        {
                            Id = 2,
                            Name = "CreateResult"
                        },
                        new
                        {
                            Id = 3,
                            Name = "UpdateResult"
                        },
                        new
                        {
                            Id = 4,
                            Name = "DeleteResult"
                        });
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.PositionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.ReceptionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateOfReturn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateReceipt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("UserId");

                    b.ToTable("Receptions");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.ResultICDEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("ICDCode")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<Guid>("ReceptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ReceptionId");

                    b.ToTable("ResultsICD");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Doctor"
                        },
                        new
                        {
                            Id = 4,
                            Name = "SeniorDoctor"
                        });
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.RolePermissionEntity", b =>
                {
                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissionEntity");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("FatherName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique()
                        .HasFilter("[AddressId] IS NOT NULL");

                    b.HasIndex("ImageId")
                        .IsUnique()
                        .HasFilter("[ImageId] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.UserRoleEntity", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoleEntity");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.DepartmentEntity", b =>
                {
                    b.HasOne("Clinic.DataAccess.Models.AddressEntity", "Address")
                        .WithOne("Department")
                        .HasForeignKey("Clinic.DataAccess.Models.DepartmentEntity", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.EmployeeDepartmentEntity", b =>
                {
                    b.HasOne("Clinic.DataAccess.Models.DepartmentEntity", null)
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clinic.DataAccess.Models.EmployeeEntity", null)
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Clinic.DataAccess.Models.PositionEntity", "Position")
                        .WithMany("EmployeeDepartments")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.EmployeeEntity", b =>
                {
                    b.HasOne("Clinic.DataAccess.Models.UserEntity", "User")
                        .WithOne("Employee")
                        .HasForeignKey("Clinic.DataAccess.Models.EmployeeEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.ReceptionEntity", b =>
                {
                    b.HasOne("Clinic.DataAccess.Models.DepartmentEntity", "Department")
                        .WithMany("Receptions")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clinic.DataAccess.Models.EmployeeEntity", "Employee")
                        .WithMany("Receptions")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Clinic.DataAccess.Models.UserEntity", "User")
                        .WithMany("Receptions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Employee");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.ResultICDEntity", b =>
                {
                    b.HasOne("Clinic.DataAccess.Models.ReceptionEntity", "Reception")
                        .WithMany("Results")
                        .HasForeignKey("ReceptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reception");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.RolePermissionEntity", b =>
                {
                    b.HasOne("Clinic.DataAccess.Models.PermissionEntity", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clinic.DataAccess.Models.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.UserEntity", b =>
                {
                    b.HasOne("Clinic.DataAccess.Models.AddressEntity", "Address")
                        .WithOne("User")
                        .HasForeignKey("Clinic.DataAccess.Models.UserEntity", "AddressId");

                    b.HasOne("Clinic.DataAccess.Models.ImageEntity", "Image")
                        .WithOne("User")
                        .HasForeignKey("Clinic.DataAccess.Models.UserEntity", "ImageId");

                    b.Navigation("Address");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.UserRoleEntity", b =>
                {
                    b.HasOne("Clinic.DataAccess.Models.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clinic.DataAccess.Models.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.AddressEntity", b =>
                {
                    b.Navigation("Department");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.DepartmentEntity", b =>
                {
                    b.Navigation("Receptions");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.EmployeeEntity", b =>
                {
                    b.Navigation("Receptions");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.ImageEntity", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.PositionEntity", b =>
                {
                    b.Navigation("EmployeeDepartments");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.ReceptionEntity", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("Clinic.DataAccess.Models.UserEntity", b =>
                {
                    b.Navigation("Employee");

                    b.Navigation("Receptions");
                });
#pragma warning restore 612, 618
        }
    }
}
