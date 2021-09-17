using OMSWebMini.Data;
using OMSWebMini.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMSWebMini.Services
{
	public class EmployeeService : IEmployeeService
	{
		public IEnumerable<Employee> GetEmployees()
		{
			var context = new NorthwindContext();
			return context.Employees.Select(e => new Employee
			{
				EmployeeId = e.EmployeeId,
				LastName = e.LastName
			});

			//var employees = context.Employees.ToList();
			//return employees.ToList();
		}

		public IEnumerable<Employee> GetIdentidiedEmployee(int id)
		{
			var context = new NorthwindContext();
			var identidiedemployee = context.Employees.ToList();
			var employee = from employe in identidiedemployee
						   where employe.EmployeeId == id
						   select employe;
			return employee;
		}
	}
}
