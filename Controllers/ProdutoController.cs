using APICatalogo.Context;
//using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace APICatalogo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository,
            IRepository<Produto> repository) 
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        //[ServiceFilter(typeof(ApiLogginFilter))]
        public  ActionResult<IEnumerable<Produto>> Get()
        {
            return _produtoRepository.GetAll().ToList();
        }

        [HttpGet("produto/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutoCategoria(int id) {
            
            var produtoByCategoria = _produtoRepository.GetProdutosByCategoria(id);

            if (produtoByCategoria == null)
            {
                return new List<Produto>();
            }

            return Ok(produtoByCategoria);
        }

        [HttpGet("{id:int:min(1)}", Name="ObterProduto")] 
        // O name é utilizado para ser chamados em outros verbos. 
        // Para por restrições, basta adicionar após o " : ", como min(), alpha (alfanuméricos), alpha:length(5) etc.

        public ActionResult<Produto> Get(int id)
        {
            var produto = _produtoRepository.GetById(c => c.ProdutoId == id);

            if (produto is null)
                return NotFound("Produto não encontrado");

            return Ok(produto);

        }

        [HttpPost("atualizar")]
        public Produto Post(Produto produto)
        {

            return _produtoRepository.Create(produto);
        }

        [HttpPut("{id}")]
        public Produto Put(int id, Produto produto)
        {
            return _produtoRepository.Update(produto);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var produto = _produtoRepository.GetById(c => c.ProdutoId == id);

            if (produto is null)
                return NotFound("Não existe esse produto");

            _produtoRepository.Delete(produto);

            return Ok(produto);
        }
    }
}
