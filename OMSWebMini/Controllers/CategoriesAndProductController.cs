using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMSWebMini.Services;

namespace OMSWebMini.Controllers
{
	[ApiController]
	public class CategoryAndProductController : ControllerBase
	{
		ICategoryAndProductService _service;
		public CategoryAndProductController(ICategoryAndProductService service)
		{
			_service = service;
		}

		[HttpGet]
		[Route("api/[controller]/category")]
		public List<Category> GetCategories()
		{
			return _service.GetCategories().ToList();
		}

		[HttpGet]
		[Route("api/[controller]/product in category")]
		public List<Product> GetProductInCategory(int id)
		{
			return _service.GetProductInCategory(id).ToList();
		}
	}
}
