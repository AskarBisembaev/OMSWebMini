using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMSWebMini.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMSWebMini.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly NorthwindContext _context;

		public OrdersController(NorthwindContext context)
		{
			_context = context;
		}
		public List<Order> GetOrders()
		{
			return (List<Order>)_context.Orders.Select(e => new Order
			{
				OrderId = e.OrderId,
			OrderDate = e.OrderDate

			}).ToList();
		}
	}
}
