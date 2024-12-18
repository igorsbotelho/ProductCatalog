using Microsoft.VisualBasic;

namespace APICatalogo.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

public class Categoria
{
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }
    [Key]
    public int CategoriaId { get; set; } // EF entende como PK
    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
        
    public ICollection<Produto>? Produtos { get; set; }
}
