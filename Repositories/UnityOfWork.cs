using APICatalogo.Context;
using APICatalogo.Repositories.Interfaces;

namespace APICatalogo.Repositories
{
    public class UnityOfWork : IUnityOfWork

    {
        private IProdutoRepository _produtoRepo;
        private ICategoriaRepository _categoriaRepo;


        private readonly AppDbContext _context;

        public UnityOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProdutoRepository ProdutoRepository { get
            {
                return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
            } 
        }

        public ICategoriaRepository CategoriaRepository {get
            {
                return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
            }
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }   
    }
}
