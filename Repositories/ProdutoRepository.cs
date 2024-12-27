using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public  IQueryable<Produto> GetProdutos()
        {
            return _context.Produtos;
        }

        public Produto GetProdutoById(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
                throw new ArgumentNullException("Produto não encontrado");

            return produto;
        }


        public Produto CreateProduto(Produto produto)
        {
            
            if (_context.Produtos.FirstOrDefault(p => p.ProdutoId == produto.ProdutoId) is not null)
                throw new ArgumentException("Esse produto já está na base");

            if (produto is null)
                throw new ArgumentNullException("Produto não pode ser nulo");

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return produto;
        }

        public Produto DeleteProduto(int id)
        {
            var produto = GetProdutoById(id);

            if (produto is null)
                throw new ArgumentNullException("Produto não existe na base");

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return produto;
        }
        public Produto UpdateProduto(Produto produto)
        {
            if (produto is null)
                throw new ArgumentNullException("Não existe");

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return produto;
        }
    }
}
