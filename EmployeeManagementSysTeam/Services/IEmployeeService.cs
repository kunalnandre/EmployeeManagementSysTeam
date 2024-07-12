using EmployeesManagementSysTeam.Models;

namespace EmployeeManagementSysTeam.Services
{
	public interface IEmployeeService
	{
		List<Employee> GetEmployeesWithAnniversaries();
	}
}
