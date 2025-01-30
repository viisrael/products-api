using System;
using ProductsApp.API.Entities;

namespace ProductsApp.API.Persistence
{
	public class ProductsDb
	{
        public ProductsDb()
        {
            Products = new List<Product>();
            Users = new List<User>();
        }

        public List<Product> Products { get; set; }
		public List<User> Users { get; set; }
	}
}

