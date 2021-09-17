using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMSWebMini.Model;

namespace OMSWebMini.Services
{
	public interface IOrderService
	{
		public IEnumerable<Order> GetOrders();
	}
}
