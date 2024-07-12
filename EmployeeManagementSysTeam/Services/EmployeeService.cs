using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeManagementSysTeam.Context;
using EmployeeManagementSysTeam.Models;
using EmployeesManagementSysTeam.Models;

namespace EmployeeManagementSysTeam.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly ApplicationDbContext _context;

		public EmployeeService(ApplicationDbContext context)
		{
			_context = context;
		}

		public List<Employee> GetEmployeesWithAnniversaries()
		{
			var today = DateTime.Today;
			return _context.Employees
				.Where(e => e.HireDate.Day == today.Day && e.HireDate.Month == today.Month)
				.ToList();
		}
	}
}
