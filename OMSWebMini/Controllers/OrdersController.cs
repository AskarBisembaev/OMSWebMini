using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMSWebMini.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMSWebMini.Services;

namespace OMSWebMini.Controllers
{
	[ApiController]
	public class OrdersController : ControllerBase
	{
		IOrderService _service;
		public OrdersController(IOrderService service)
		{
			_service = service;
		}
		[HttpGet]
	[Route("api/[controller]/orders")]
		public List<Order> GetOrders()
		{
			return _service.GetOrders().ToList();
		}
	}
}
