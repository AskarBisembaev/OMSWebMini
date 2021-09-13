using System;
using OMSWebMini.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMSWebMini.services
{
	public interface ICategorySevice
	{
		public IEnumerable<Category> GetCategories();
		public IEnumerable<Product> GetProduct(int id);
	}
}
