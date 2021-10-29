using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMSWebMini.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OMSWebMini.Model;

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
		public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
		{
			return await _context.Orders.Select(o => new Order
			{
				OrderId = o.OrderId,
				OrderDate = o.OrderDate,
				CustomerId = o.CustomerId,
				EmployeeId = o.EmployeeId
			}).ToListAsync();
		}

		[HttpGet]
		[Route("api/[controller]/GetOrdersWithID")]
		public async Task<ActionResult<Order>> GetOrder(int id)
		{
			var order = await _context.Orders
			   .Where(o => o.OrderId == id)
			   .FirstOrDefaultAsync();

			if (order == null) return NotFound();
			return order;
		}

		[HttpPost]
		[Route("api/[controller]/PostOrder")]
		public async Task<ActionResult<Order>> PostOrder(Order order)
		{
			_context.Orders.Add(order);
			await _context.SaveChangesAsync();
			var result = CreatedAtAction(nameof(GetOrder),
				new
				{ Id = order.OrderId },
				order);
			return result;
		}

		[HttpPut]
		[Route("api/[controller]/PutOrder")]
		public async Task<IActionResult> PutOrder(int id, Order item)
		{
			if (id != item.OrderId)
			{
				return BadRequest();
			}
			_context.Entry(item).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return NoContent();
		}


		[HttpDelete]
		[Route("api/[controller]/DeleteOrder")]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			var order = await _context.Orders.FindAsync(id);

			if (order == null)
			{
				return NotFound();
			}

			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var details = _context.OrderDetails.Where(o =>order.OrderId == id);
					_context.OrderDetails.RemoveRange(details);
				_context.Orders.Remove(order);
     		await _context.SaveChangesAsync();
			await transaction.CommitAsync();
				}
				catch (Exception)
				{
					await transaction.RollbackAsync();
				}
			}

			return NoContent();
		}

		[HttpDelete]
		[Route("api/[controller]/DeleteOrders")]
		public async Task<IActionResult> DeleteOrdersRange(int[] range)
		{
			List<Order> orders = new List<Order>();
			List<OrderDetails> details = new List<OrderDetails>();

			foreach (int id in range)
			{
				var order = await _context.Orders.FindAsync(id);
				if (order != null) orders.Add(order);
			}

			if (orders.Count == 0) return NotFound();

			foreach (var item in orders)
			{
				var detail = _context.OrderDetails.Where(o => o.OrderId == item.OrderId) as OrderDetails;
				if (detail != null) details.Add(detail);
			}

			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					if (details.Count != 0) _context.OrderDetails.RemoveRange(details);
					_context.Orders.RemoveRange(orders);
					await transaction.CommitAsync();
				}
				catch (Exception)
				{
				await transaction.RollbackAsync();
				}
				await _context.SaveChangesAsync();
			}
			return NoContent();
		}
	}
}
