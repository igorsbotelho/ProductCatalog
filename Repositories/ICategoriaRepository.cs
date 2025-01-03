﻿using APICatalogo.Models;

namespace APICatalogo.Repositories
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> GetCategorias();
        Categoria GetCategoriaById(int id);
        Categoria CreateCategoria(Categoria categoria);
        Categoria UpdateCategoria(Categoria categoria);
        Categoria DeleteCategoria(int id);
    }
}
