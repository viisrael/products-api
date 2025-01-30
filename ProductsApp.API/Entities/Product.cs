using System;
using ProductsApp.API.Persistence;

namespace ProductsApp.API.Entities
{
	//	POST /USERS
	//Params: nome, email e senha

	//PUT /USERS/ID
	//Params: nome

	//Delete /USERS/ID
	//Params: usuarioid

	//POST /LOGIN
	//Params: email e senha
	//Return: accesstoken

	//POST /PRODUCTS
	//Params: ProdutoId, Descrição, Estoque, BarCode, UnidadeMedida, Preco

	//PUT /PRODUCTS/ID
	//Params: Descrição, Estoque, BarCode, UnidadeMedida, Preco, AtualizadoEm

	//GET /PRODUCTS
    public class Product
    {
		public Product() { }

        public Product(Guid productId, string descricao, int estoque, string barCode, string unidadeMedida, decimal preco, DateTime atualizadoEm)
        {
            ProductId = productId;
            Descricao = descricao;
            Estoque = estoque;
            BarCode = barCode;
            UnidadeMedida = unidadeMedida;
            Preco = preco;
            AtualizadoEm = atualizadoEm;
        }

        public Guid ProductId { get; set; }
		public string Descricao { get; set; }
		public int Estoque { get; set; }
		public string BarCode { get; set; }
		public string UnidadeMedida { get; set; }
		public decimal Preco { get; set; }
		public DateTime AtualizadoEm { get; set; }

		public void Update(string descricao, int estoque, string barCode, string unidadeMedida, decimal preco)
		{
			Descricao = descricao;
            Estoque = estoque;
            BarCode = barCode;
            UnidadeMedida = unidadeMedida;
            Preco = preco;
			AtualizadoEm = DateTime.UtcNow;
        }
	}
}

