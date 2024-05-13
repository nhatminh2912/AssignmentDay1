using EFCoreAssignmentDay1.Configs;
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

            modelBuilder.Entity<Department>().HasData(
            new Department { Id = Guid.NewGuid(), Name = "Accounting" },
            new Department { Id = Guid.NewGuid(), Name = "Finance" },
            new Department { Id = Guid.NewGuid(), Name = "HR" },
            new Department { Id = Guid.NewGuid(), Name = "Software Development" }
            );
        }
    }
}