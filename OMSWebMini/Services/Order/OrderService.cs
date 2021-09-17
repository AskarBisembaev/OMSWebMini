using OMSWebMini.Data;
using OMSWebMini.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMSWebMini.Services
{
	public class OrderService : IOrderService
	{
		public IEnumerable<Order> GetOrders()
		{
			var context = new NorthwindContext();
			var order = context.Orders.ToList();
			return order;
		}
	}
}
