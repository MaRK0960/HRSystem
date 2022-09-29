using Microsoft.EntityFrameworkCore;

namespace HR_Employees.Models.DBModels
{
    public class HRContext : DbContext
    {
        public HRContext(DbContextOptions<HRContext> options)
            : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasCheckConstraint("CK_Employee_ManagerID", $"{nameof(Employee.ID)} != {nameof(Employee.ManagerID)}")
                .ToTable("Employees");

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Name);

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.ManagerID)
                .HasFilter($"{nameof(Employee.ManagerID)} is not null");

            modelBuilder.Entity<Log>()
                .ToTable("Logs");

            modelBuilder.Entity<Log>()
                .HasIndex(l => l.EmployeeID);
        }
    }
}