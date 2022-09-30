using HR_Employees.Models.DBModels.Entities;
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
				.ToTable("Employees");

			modelBuilder.Entity<Employee>()
				.HasOne(e => e.Manager)
				.WithMany()
				.HasForeignKey(e => e.ManagerID);

			modelBuilder.Entity<Employee>()
				.HasCheckConstraint("CK_Employee_ManagerID", $"{nameof(Employee.ID)} != {nameof(Employee.ManagerID)}");

			modelBuilder.Entity<Employee>()
				.HasIndex(e => e.Name);

			modelBuilder.Entity<Employee>()
				.HasIndex(e => e.ManagerID)
				.HasFilter($"{nameof(Employee.ManagerID)} is not null");

			modelBuilder.Entity<Log>()
				.ToTable("Logs");

			modelBuilder.Entity<Log>()
				.HasOne(l => l.Employee)
				.WithMany(e => e.Logs)
				.HasForeignKey(l => l.EmployeeID);

			modelBuilder.Entity<Log>()
				.HasIndex(l => l.EmployeeID);
		}
	}
}