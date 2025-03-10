using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
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
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            var categorias = _unityOfWork.CategoriaRepository.GetAll();

            if (categorias is null)
                return NotFound("Não encontrei categorias");

            var categoriasDto = categorias.ToCategoriaDTOList();

            return Ok(categoriasDto); 
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> GetById(int id)
        {
            var categoria = _unityOfWork.CategoriaRepository.GetById(c => c.CategoriaId == id);
            
            if (categoria is null)
                return NotFound("Produto não encontrado");


            var categoriaDto = categoria.ToCategoriaDTO();

            return Ok(categoriaDto);
        }

        [HttpPut("{id}")]

        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId)
                return BadRequest();

            var categoria = categoriaDto.ToCategoria();

            var categoriaAtualizada = _unityOfWork.CategoriaRepository.Update(categoria);
            _unityOfWork.Commit();

            var novaCategoriaDto = categoriaAtualizada.ToCategoriaDTO();

            return Ok(novaCategoriaDto);
        }





        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
                return BadRequest();

            var categoria = categoriaDto.ToCategoria();

            var categoriaCriada = _unityOfWork.CategoriaRepository.Create(categoria);
            _unityOfWork.Commit();


            var novaCategoriaDto = categoriaCriada.ToCategoriaDTO();


            return Ok(novaCategoriaDto);

            // new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria)
        }

        [HttpDelete]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _unityOfWork.CategoriaRepository.GetById(c => c.CategoriaId == id); 

            if (categoria is null)
                return NotFound("Categoria não existe");
            
            var categoriaExcluida = _unityOfWork.CategoriaRepository.Delete(categoria);
            _unityOfWork.Commit();

            var categoriaExcluidaDto = categoriaExcluida.ToCategoriaDTO();

            return Ok(categoriaExcluidaDto);
        }
    }
}
