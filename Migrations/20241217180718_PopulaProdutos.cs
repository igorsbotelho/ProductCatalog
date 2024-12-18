using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                   "Values ('Produto 1', 'Descrição do Produto 1', 100.00, 'imagem.jpg', 50.0, now(), 3)");

            mb.Sql("Insert into Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                   "Values ('Produto 2', 'Descrição do Produto 2', 200.00, 'imagem.jpg', 30.0, now(), 3)");

            mb.Sql("Insert into Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                   "Values ('Produto 3', 'Descrição do Produto 3', 150.00, 'imagem.jpg', 20.0, now(), 4)");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Produtos");
        }
    }
}
