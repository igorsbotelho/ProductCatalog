using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace APICatalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;

        public readonly AppDbContext _context;
        public readonly IConfiguration _configuration;

        public CategoriaController(ICategoriaRepository repository, AppDbContext context, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
            _context = context;
        }

        [HttpGet("ReadConfigFiles")]
        public string GetValores()
        {
            var valores = _configuration["chave1"];

            return $"Chave 1 = {valores}";
        }

        [HttpGet("produtos")]

        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            return _context.Categorias.Include(p => p.Produtos).ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _repository.GetCategorias();

            return Ok(categorias); 
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetById(int id)
        {
            var categoria = _repository.GetCategoriaById(id);

            if (categoria is null)
                return NotFound("Produto não encontrado");

            return Ok(categoria);
        }

        [HttpPut("{id}")]

        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest();

            _repository.UpdateCategoria(categoria); 

            return Ok(categoria);
        }

        [HttpPost("{id}")]

        public ActionResult Post(int id, Categoria categoria)
        {
            if (categoria is null)
                return BadRequest();

            var categoriaCriada = _repository.CreateCategoria(categoria);

            return Ok(categoriaCriada);

            // new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria)
        }

        public ActionResult Delete(int id)
        {
            var categoria = _repository.GetCategoriaById(id); 

            if (categoria is null)
                return NotFound("Categoria não existe");
            
            var categoriaExcluida = _repository.DeleteCategoria(id);
            return Ok(categoria);
        }
    }
}
