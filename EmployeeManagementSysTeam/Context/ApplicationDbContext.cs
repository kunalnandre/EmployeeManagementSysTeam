using EmployeesManagementSysTeam.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSysTeam.Context
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions)
		{

		}
		public DbSet<Employee> Employees { get; set; }
	}
}
