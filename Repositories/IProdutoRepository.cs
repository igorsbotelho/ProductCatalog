using APICatalogo.Models;

namespace APICatalogo.Repositories
{
    public interface IProdutoRepository
    {
        public IQueryable<Produto> GetProdutos();
        public Produto GetProdutoById(int id);
        public Produto CreateProduto(Produto produto);
        public Produto UpdateProduto(Produto produto);
        public Produto DeleteProduto(int id);
    }
}
