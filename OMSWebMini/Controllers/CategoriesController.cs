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
	public class CategoryController : ControllerBase
	{
		private readonly NorthwindContext _context;
		public CategoryController(NorthwindContext context)
		{
			_context = context;
		}

		[HttpGet]
		[Route("api/[controller]/categoryAsync")]
		public async Task<ActionResult<IEnumerable<Category>>> GetCategory(bool picture = false)
		{
			if (picture)
			{
				return await _context.Categories.Select(c => new Category
				{
					CategoryId = c.CategoryId,
					CategoryName = c.CategoryName,
					Picture = c.Picture
				}).ToListAsync();
			}
			else
			{
				return await _context.Categories.Select(c => new Category
				{
					CategoryId = c.CategoryId,
					CategoryName = c.CategoryName,
				}).ToListAsync();
			}
		}

		[HttpGet]
		[Route("api/[controller]/category_with_ID")]
		public async Task<ActionResult<Category>> GetIDCategory(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category == null)
			{
				return NotFound();
			}
			else
			{
				return new Category
				{
					CategoryId = category.CategoryId,
					CategoryName = category.CategoryName,
				};
			}
		}

		[HttpPost]
		[Route("api/[controller]/post_category")]
		public async Task<ActionResult<IEnumerable<Category>>> PostCategory(Category category)
		{
			Category category1 = new Category()
			{
				CategoryName = category.CategoryName,
				Description = category.Description
			};
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetCategory),
				new
				{
					Id = category.CategoryId
				},
				category1);
		}

		[HttpPut]
		[Route("api/[controller]/put_category")]
		public async Task<IActionResult> PutCategory(int id, Category category)
		{
			if (id != category.CategoryId)
			{
				return BadRequest();
			}

			_context.Entry(category).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CategoryExists(id))
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
		private bool CategoryExists(int id)
		{
			return _context.Categories.Any(e => e.CategoryId == id);
		}

		[HttpDelete]
		[Route("api/[controller]/delete_category")]
		public async Task<ActionResult<Category>> DeleteCategory(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category == null)
			{
				return NotFound();
			}

			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();

			return category;
		}
	}
}
