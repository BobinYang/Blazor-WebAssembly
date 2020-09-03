using System;
using Covid.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using DailyHealth = Covid.Server.Entities.DailyHealth;
using Department = Covid.Server.Entities.Department;
using Employee = Covid.Server.Entities.Employee;

namespace Covid.Server.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyHealth>().HasKey(x => new
            {
                x.EmployeeId,
                x.Date
            });

            modelBuilder.Entity<Employee>().HasOne<Department>().WithMany().HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DailyHealth>().HasOne<Employee>().WithMany().HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "研发部" },
                new Department { Id = 2, Name = "销售部" },
                new Department { Id = 3, Name = "采购部" });

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    DepartmentId = 1, 
                    No = "A01", 
                    Name = "Nick Carter", 
                    PictureUrl = "", 
                    BirthDate = new DateTime(1980, 1,1),
                    Gender = Gender.Male
                }, 
                new Employee
                {
                    Id = 2,
                    DepartmentId = 1,
                    No = "A02",
                    Name = "Mike Seaver",
                    PictureUrl = "",
                    BirthDate = new DateTime(1975, 4, 5),
                    Gender = Gender.Male
                },
                new Employee
                {
                    Id = 3,
                    DepartmentId = 2,
                    No = "B01",
                    Name = "Sarah Jackson",
                    PictureUrl = "",
                    BirthDate = new DateTime(1989, 4, 7),
                    Gender = Gender.Female
                },
                new Employee
                {
                    Id = 4,
                    DepartmentId = 2,
                    No = "B02",
                    Name = "Mary Bloody",
                    PictureUrl = "",
                    BirthDate = new DateTime(1995, 11, 3),
                    Gender = Gender.Female
                }
                ,
                new Employee
                {
                    Id = 5,
                    DepartmentId = 3,
                    No = "C01",
                    Name = "Joe Kent",
                    PictureUrl = "",
                    BirthDate = new DateTime(1979, 7, 12),
                    Gender = Gender.Male
                },
                new Employee
                {
                    Id = 6,
                    DepartmentId = 3,
                    No = "C02",
                    Name = "Axl",
                    PictureUrl = "",
                    BirthDate = new DateTime(1961, 1, 14),
                    Gender = Gender.Male
                });
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<DailyHealth> DailyHealths { get; set; }
    }
}
