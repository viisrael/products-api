using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsApp.API.Entities;
using ProductsApp.API.Persistence;

namespace ProductsApp.API.Controllers
{
	[ApiController]
	[Route("api/products")]
	[Authorize]
	public class ProductsController : ControllerBase
	{
		private readonly ProductsDb _db;

		public ProductsController(ProductsDb db)
		{
			_db = db;
		}

		[HttpPost]
		public IActionResult Post(Product product)
		{
			_db.Products.Add(product);

			return NoContent();
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			return Ok(_db.Products);
		}

        [HttpGet("{barcode}")]
        public IActionResult GetByBarCode(string barcode)
        {
			var product = _db.Products.SingleOrDefault(p => p.BarCode == barcode);

			if (product is null)
				return NotFound();

            return Ok(product);
        }

        [HttpPut("{id}")]
		public IActionResult Put(Guid id, ProductUpdateInputModel model)
		{
			var dbProduct = _db.Products.SingleOrDefault(p => p.ProductId == id);

			if (dbProduct == null)
				return NotFound();

			dbProduct.Update(model.Descricao, model.Estoque, model.BarCode, model.UnidadeMedida, model.Preco);

			return NoContent();
		}
	}

	public class ProductUpdateInputModel
	{
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        public string BarCode { get; set; }
        public string UnidadeMedida { get; set; }
        public decimal Preco { get; set; }
    }
}

