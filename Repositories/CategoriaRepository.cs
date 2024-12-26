using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<Categoria> GetCategorias()
        {
            var categorias = _context.Categorias.ToList();

            return categorias;
        }

        public Categoria GetCategoriaById(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if (categoria is null)
                return null;

            return categoria;
        }

        public Categoria CreateCategoria(Categoria categoria)
        {
            if (categoria is null)
                throw new ArgumentNullException(nameof(categoria));

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return categoria;
        }

        public Categoria DeleteCategoria(int id)
        {
            var categoria = GetCategoriaById(id);

            if (categoria is null)
                throw new ArgumentNullException(nameof(categoria));

            _context.Categorias.Remove(categoria);

            return categoria;
        }

        public Categoria UpdateCategoria(Categoria categoria)
        {
            if (categoria is null)
                throw new ArgumentNullException(nameof(categoria));

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return categoria;
        }
    }
}
