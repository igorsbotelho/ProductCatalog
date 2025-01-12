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
        private readonly IUnityOfWork _unityOfWork;

        public ProdutoController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        //[ServiceFilter(typeof(ApiLogginFilter))]
        public  ActionResult<IEnumerable<Produto>> Get()
        {
            return _unityOfWork.ProdutoRepository.GetAll().ToList();
        }

        [HttpGet("produto/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutoCategoria(int id) {
            
            var produtoByCategoria = _unityOfWork.ProdutoRepository.GetProdutosByCategoria(id);

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
            var produto = _unityOfWork.ProdutoRepository.GetById(c => c.ProdutoId == id);

            if (produto is null)
                return NotFound("Produto não encontrado");

            return Ok(produto);

        }

        [HttpPost("atualizar")]
        public ActionResult<Produto> Post(Produto produto)
        {
            if (produto is null)
                return NotFound("Não encontrado o produto");

            _unityOfWork.ProdutoRepository.Create(produto);
            _unityOfWork.Commit();

            return Ok(produto);
        }

        [HttpPut("{id}")]
        public ActionResult<Produto> Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
                return NotFound("Produto de id {id} não encontrado");

            _unityOfWork.ProdutoRepository.Update(produto);
            _unityOfWork.Commit();

            return Ok(produto);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var produto = _unityOfWork.ProdutoRepository.GetById(c => c.ProdutoId == id);

            if (produto is null)
                return NotFound("Não existe esse produto");

            _unityOfWork.ProdutoRepository.Delete(produto);
            _unityOfWork.Commit();
            return Ok(produto);
        }
    }
}
