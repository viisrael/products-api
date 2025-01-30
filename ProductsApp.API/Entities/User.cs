using System;
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

    public class User
	{
        public User(Guid usuarioId, string nome, string email, string senha)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Email = email;
            Senha = senha;
        }

        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}

