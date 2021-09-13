using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMSWebMini.services;

namespace OMSWebMini.Controllers
{
	[ApiController]
	public class CategoryAndProductController : ControllerBase
	{
		ICategorySevice _service3;
		public CategoryAndProductController(ICategorySevice service)
		{
			_service3 = service;
		}

		[HttpGet]
		[Route("api/[controller]/category")]
		public List<Category> GetCategories()
		{
			return _service3.GetCategories().ToList();
		}

		[HttpGet]
		[Route("api/[controller]/product")]
		public List<Product> GetProduct(int id)
		{
			return _service3.GetProduct(id).ToList();
		}
	}
}
