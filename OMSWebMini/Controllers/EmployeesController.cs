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
	[ApiController]
	public class EmployeesController : ControllerBase
	{
		private readonly NorthwindContext _context;
		public EmployeesController(NorthwindContext context)
		{
			_context = context;
		}

		[HttpGet]
		[Route("api/[controller]/GetEmployeeAsync")]
		public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
		{
			return await _context.Employees.Select(e => new Employee
			{
				EmployeeId = e.EmployeeId,
				FirstName = e.FirstName,
				LastName = e.LastName,
				HomePhone = e.HomePhone
			}).ToListAsync();
		}
	}
}
