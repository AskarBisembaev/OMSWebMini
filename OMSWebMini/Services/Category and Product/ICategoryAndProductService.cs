using System;
using OMSWebMini.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMSWebMini.Services
{
	public interface ICategoryAndProductService
	{
		public IEnumerable<Category> GetCategories();
		public IEnumerable<Product> GetProductInCategory(int id);
	}
}
