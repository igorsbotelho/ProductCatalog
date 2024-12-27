using APICatalogo.Context;
//using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace APICatalogo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController
    {
        private readonly IProdutoRepository _repository;

        public ProdutoController(IProdutoRepository repository) 
        {
            _repository = repository;
        }

        [HttpGet]
        //[ServiceFilter(typeof(ApiLogginFilter))]
        public  ActionResult<IEnumerable<Produto>> Get()
        {
            return _repository.GetProdutos().ToList();
        }

        [HttpGet("{id:int:min(1)}", Name="ObterProduto")] 
        // O name é utilizado para ser chamados em outros verbos. 
        // Para por restrições, basta adicionar após o " : ", como min(), alpha (alfanuméricos), alpha:length(5) etc.

        public ActionResult<Produto> Get(int id)
        {
            return _repository.GetProdutoById(id);
        }

        [HttpPost("atualizar")]
        public Produto Post(Produto produto)
        {

            return _repository.CreateProduto(produto);
        }

        [HttpPut("{id}")]
        public Produto Put(int id, Produto produto)
        {
            return _repository.UpdateProduto(produto);
        }

        [HttpDelete("{id}")]
        public Produto Delete(int id)
        {
            return _repository.DeleteProduto(id);
        }
    }
}
