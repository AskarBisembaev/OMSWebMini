using OMSWebMini.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMSWebMini.services
{
	public class CategoryServices : ICategorySevice
	{
			public IEnumerable<Category> GetCategories()
			{
				var context = new NorthwindContext();
				var categories = context.Categories.ToList();
				return categories;
			}

			public IEnumerable<Product> GetProduct(int id)
			{
				var context = new NorthwindContext();
				var products = context.Products.ToList();
				var product = from prod in products
							  where prod.CategoryId == id
							  select prod;
				return product;
			}

		}
	}


