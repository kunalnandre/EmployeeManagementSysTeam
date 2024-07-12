using EmployeeManagementSysTeam.Models;
using EmployeesManagementSysTeam.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSysTeam.Context 
{
    public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext()
        {   
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions)
		{
		}
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Admin> LoginViewModel { get; set; }
    }   
}
