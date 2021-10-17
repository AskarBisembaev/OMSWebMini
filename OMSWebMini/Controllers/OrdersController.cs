using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMSWebMini.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OMSWebMini.Controllers
{
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly NorthwindContext _context;
		public OrdersController(NorthwindContext context)
		{
			_context = context;
		}

		[HttpGet]
		[Route("api/[controller]/GetOrdersAsync")]
		public async Task<ActionResult<IEnumerable<Order>>> GetEmployee()
		{
			return await _context.Orders.Select(o => new Order
			{
				OrderId = o.OrderId,
				OrderDate = o.OrderDate,
				CustomerId = o.CustomerId,
				EmployeeId = o.EmployeeId
			}).ToListAsync();
		}

	}
}
