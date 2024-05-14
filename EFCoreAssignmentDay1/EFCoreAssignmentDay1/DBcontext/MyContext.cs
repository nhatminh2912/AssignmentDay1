using EFCoreAssignmentDay1.Configs;
using EFCoreAssignmentDay1.DTO;
using EFCoreAssignmentDay1.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAssignmentDay1.DBcontext
{
    public class MyContext : DbContext
    {
        private readonly DatabaseConnections _databaseConnections;
        public MyContext(DbContextOptions<MyContext> options, DatabaseConnections databaseConnections) : base(options)
        {
            _databaseConnections = databaseConnections;
        }
        public DbSet<EmployeeWithDepartmentDto> EmployeeWithDepartmentDto { get; set; }
        public DbSet<EmployeeWithProjectsDto> EmployeeWithProjectsDto { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_databaseConnections.ConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the one-to-one relationship
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Salary)
                .WithOne(s => s.Employee)
                .HasForeignKey<Salary>(s => s.EmployeeId);

            // Configuring the one-to-many relationship
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);

            // Configuring the many-to-many relationship
            modelBuilder.Entity<ProjectEmployee>()
                .HasKey(pe => new { pe.ProjectId, pe.EmployeeId });
            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Project)
                .WithMany(p => p.ProjectEmployees)
                .HasForeignKey(pe => pe.ProjectId);
            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Employee)
                .WithMany(e => e.ProjectEmployees)
                .HasForeignKey(pe => pe.EmployeeId);

            // Ensure no table is created for DbQuery properties
            modelBuilder.Ignore<EmployeeWithDepartmentDto>();
            modelBuilder.Ignore<EmployeeWithProjectsDto>();

            // Seeding data
            var departments = new List<Department>
        {
            new Department { Id = Guid.NewGuid(), Name = "Software Development" },
            new Department { Id = Guid.NewGuid(), Name = "Finance" },
            new Department { Id = Guid.NewGuid(), Name = "Accountant" },
            new Department { Id = Guid.NewGuid(), Name = "HR" }
        };

            var projects = new List<Project>
        {
            new Project { Id = Guid.NewGuid(), Name = "Project A" },
            new Project { Id = Guid.NewGuid(), Name = "Project B" }
        };

            var employees = new List<Employee>
        {
            new Employee { Id = Guid.NewGuid(), Name = "John Doe", DepartmentId = departments[0].Id, JoinedDate = new DateTime(2023, 6, 1) },
            new Employee { Id = Guid.NewGuid(), Name = "Jane Smith", DepartmentId = departments[1].Id, JoinedDate = new DateTime(2024, 1, 1) }
        };

            var salaries = new List<Salary>
        {
            new Salary { Id = Guid.NewGuid(), EmployeeId = employees[0].Id, Amount = 1500f },
            new Salary { Id = Guid.NewGuid(), EmployeeId = employees[1].Id, Amount = 2000f }
        };

            var projectEmployees = new List<ProjectEmployee>
        {
            new ProjectEmployee { ProjectId = projects[0].Id, EmployeeId = employees[0].Id, Enable = true },
            new ProjectEmployee { ProjectId = projects[1].Id, EmployeeId = employees[1].Id, Enable = true }
        };

            modelBuilder.Entity<Department>().HasData(departments);
            modelBuilder.Entity<Project>().HasData(projects);
            modelBuilder.Entity<Employee>().HasData(employees);
            modelBuilder.Entity<Salary>().HasData(salaries);
            modelBuilder.Entity<ProjectEmployee>().HasData(projectEmployees);
        }
    }
}