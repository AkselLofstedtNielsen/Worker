﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkerPlatform.API.Data;

#nullable disable

namespace WorkerPlatform.API.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmployeeWorkField", b =>
                {
                    b.Property<int>("EmployeesId")
                        .HasColumnType("int");

                    b.Property<int>("WorkFieldsId")
                        .HasColumnType("int");

                    b.HasKey("EmployeesId", "WorkFieldsId");

                    b.HasIndex("WorkFieldsId");

                    b.ToTable("EmployeeWorkField");
                });

            modelBuilder.Entity("WorkerPlatform.API.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("WorkerPlatform.API.Models.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("WorkerPlatform.API.Models.WorkField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("WorkFields");
                });

            modelBuilder.Entity("EmployeeWorkField", b =>
                {
                    b.HasOne("WorkerPlatform.API.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkerPlatform.API.Models.WorkField", null)
                        .WithMany()
                        .HasForeignKey("WorkFieldsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkerPlatform.API.Models.WorkField", b =>
                {
                    b.HasOne("WorkerPlatform.API.Models.Manager", "Manager")
                        .WithMany("WorkFields")
                        .HasForeignKey("ManagerId");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("WorkerPlatform.API.Models.Manager", b =>
                {
                    b.Navigation("WorkFields");
                });
#pragma warning restore 612, 618
        }
    }
}
