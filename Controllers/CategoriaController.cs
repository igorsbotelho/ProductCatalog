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
        private readonly IUnityOfWork _unityOfWork;

        public CategoriaController(IUnityOfWork unityOfWork, IConfiguration configuration)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _unityOfWork.CategoriaRepository.GetAll();

            return Ok(categorias); 
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetById(int id)
        {
            var categoria = _unityOfWork.CategoriaRepository.GetById(c => c.CategoriaId == id);

            if (categoria is null)
                return NotFound("Produto não encontrado");

            return Ok(categoria);
        }

        [HttpPut("{id}")]

        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest();

            _unityOfWork.CategoriaRepository.Update(categoria); 

            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
                return BadRequest();

            var categoriaCriada = _unityOfWork.CategoriaRepository.Create(categoria);

            return Ok(categoriaCriada);

            // new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria)
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var categoria = _unityOfWork.CategoriaRepository.GetById(c => c.CategoriaId == id); 

            if (categoria is null)
                return NotFound("Categoria não existe");
            
            var categoriaExcluida = _unityOfWork.CategoriaRepository.Delete(categoria);
            return Ok(categoria);
        }
    }
}
