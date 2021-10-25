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
		public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
		{
			return await _context.Employees.Select(e => new Employee
			{
				EmployeeId = e.EmployeeId,
				FirstName = e.FirstName,
				LastName = e.LastName,
				HomePhone = e.HomePhone
			}).ToListAsync();
		}

		[HttpGet]
		[Route("api/[controller]/GetEmployeeID")]
		public async Task<ActionResult<Employee>> GetEmployee(int id)
		{
			var employee = await _context.Employees.FindAsync(id);
			if (employee == null)
			{
				return NotFound();
			}
			return employee;
		}


		[HttpPost]
		[Route("api/[controller]/EmployeePost")]
		public async Task<ActionResult<Employee>> PostEmployee(Employee employees)
		{
			Employee employee = new Employee()
			{
				FirstName = employees.FirstName,
				LastName = employees.LastName
			};
			_context.Employees.Add(employees);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetEmployees),
				new
				{
					id = employee.EmployeeId
				},
				employee);
		}


		[HttpPut]
		[Route("api/[controller]/EmployeePut")]
		public async Task<ActionResult> PutEmployee(int id, Employee employee)
		{
			if (id != employee.EmployeeId)
			{
				return BadRequest();
			}
			_context.Entry(employee).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!EmployeeExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return NoContent();
		}

		private bool EmployeeExists(int id)
		{
			return _context.Employees.Any(e => e.EmployeeId == id);
		}

		[HttpDelete]
		[Route("api/[controller]/EmployeeDelete")]
		public async Task<ActionResult<Employee>> EmployeeDelete(int id)
		{
			var employee = await _context.Employees.FindAsync(id);
			if (employee == null)
			{
				return NotFound();
			}

			_context.Employees.Remove(employee);
			await _context.SaveChangesAsync();

			return employee;
		}




	}
}
