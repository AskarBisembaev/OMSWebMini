using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMSWebMini.Data;
using OMSWebMini.Model;

namespace OMSWebMini.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
		private readonly NorthwindContext _context;

		public EmployeesController(NorthwindContext context)
		{
			_context = context;
		}

		[HttpGet]
		[Route("api/[controller]/employee")]
		public List<Employee> GetEmployees()
		{
			return (List<Employee>)_context.Employees.Select(e => new Employee
			{
				EmployeeId = e.EmployeeId,
				LastName = e.LastName

			}).ToList();
		}

		[HttpGet]
		[Route("api/[controller]/employeeid")]
		public List<Employee> GetEmployee(int id)
		{
			var employee = _context.Employees;
			var employeeid = from employ in employee
							 where employ.EmployeeId == id
							 select employ;
			return employeeid.ToList();
		}
	}
}
