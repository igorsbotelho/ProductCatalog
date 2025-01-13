using APICatalogo.Context;
using APICatalogo.DTOs;

//using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProdutoController(IUnityOfWork unityOfWork, IMapper mapper)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        //[ServiceFilter(typeof(ApiLogginFilter))]
        public  ActionResult<IEnumerable<ProdutoDTO>> Get()
        {
            var produtos = _unityOfWork.ProdutoRepository.GetAll().ToList();

            if (produtos is null)
                return NotFound("Não encontrei produtos");

            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return Ok(produtosDto);
        }

        [HttpGet("produto/{id}")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutoCategoria(int id) {
            
            var produtoByCategoria = _unityOfWork.ProdutoRepository.GetProdutosByCategoria(id);

            if (produtoByCategoria == null)
            {
                return new List<ProdutoDTO>();
            }

            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtoByCategoria);


            return Ok(produtosDto);
        }

        [HttpGet("{id:int:min(1)}", Name="ObterProduto")] 
        // O name é utilizado para ser chamados em outros verbos. 
        // Para por restrições, basta adicionar após o " : ", como min(), alpha (alfanuméricos), alpha:length(5) etc.

        public ActionResult<ProdutoDTO> Get(int id)
        {
            var produto = _unityOfWork.ProdutoRepository.GetById(c => c.ProdutoId == id);

            if (produto is null)
                return NotFound("Produto não encontrado");

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDto);
        }

        [HttpPost("atualizar")]
        public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDto)
        {
            if (produtoDto is null)
                return NotFound("Não encontrado o produto");

            var produto = _mapper.Map<Produto>(produtoDto);

            _unityOfWork.ProdutoRepository.Create(produto);
            _unityOfWork.Commit();

            var produtoDtoCriado = _mapper.Map<ProdutoDTO>(produto);
            return Ok(produtoDtoCriado);
        }

        [HttpPut("{id}")]
        public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDto)
        {
            if (id != produtoDto.ProdutoId)
                return NotFound("Produto de id {id} não encontrado");

            var produto = _mapper.Map<Produto>(produtoDto); 

            var produtoAtualizado = _unityOfWork.ProdutoRepository.Update(produto);
            _unityOfWork.Commit();

            var produtoDtoAtualizado = _mapper.Map<ProdutoDTO>(produtoAtualizado);

            return Ok(produtoDtoAtualizado);
        }

        [HttpDelete("{id}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
            var produto = _unityOfWork.ProdutoRepository.GetById(c => c.ProdutoId == id);

            if (produto is null)
                return NotFound("Não existe esse produto");

            var produtoDeletado = _unityOfWork.ProdutoRepository.Delete(produto);
            _unityOfWork.Commit();

            var produtoDeletatoDto = _mapper.Map<ProdutoDTO>(produtoDeletado);

            return Ok(produtoDeletatoDto);
        }
    }
}
