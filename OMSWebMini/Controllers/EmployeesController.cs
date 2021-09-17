using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMSWebMini.Data;
using OMSWebMini.Model;
using OMSWebMini.Services;

namespace OMSWebMini.Controllers
{
    [ApiController]
    public class EmployeesController : ControllerBase
    {
		IEmployeeService _service;
		public EmployeesController(IEmployeeService service)
		{
			_service = service;
		}

		[HttpGet]
		[Route("api/[controller]/employees")]
		public List<Employee> GetEmployees()
		{
			return _service.GetEmployees().ToList();
		}

		[HttpGet]
		[Route("api/[controller]/identidied employee")]
		public List<Employee> GetEmployee(int id)
		{
			return _service.GetIdentidiedEmployee(id).ToList();
		}
	}
}
