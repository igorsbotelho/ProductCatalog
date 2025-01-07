using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
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
        private readonly IRepository<Categoria> _repository;
        public readonly IConfiguration _configuration;

        public CategoriaController(ICategoriaRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _repository.GetAll();

            return Ok(categorias); 
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetById(int id)
        {
            var categoria = _repository.GetById(c => c.CategoriaId == id);

            if (categoria is null)
                return NotFound("Produto não encontrado");

            return Ok(categoria);
        }

        [HttpPut("{id}")]

        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest();

            _repository.Update(categoria); 

            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
                return BadRequest();

            var categoriaCriada = _repository.Create(categoria);

            return Ok(categoriaCriada);

            // new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria)
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var categoria = _repository.GetById(c => c.CategoriaId == id); 

            if (categoria is null)
                return NotFound("Categoria não existe");
            
            var categoriaExcluida = _repository.Delete(categoria);
            return Ok(categoria);
        }
    }
}
