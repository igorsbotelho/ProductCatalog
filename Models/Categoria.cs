using Microsoft.VisualBasic;

namespace APICatalogo.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

public class Categoria : IValidatableObject
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

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {   
        if (!string.IsNullOrEmpty(this.Nome))
        {
            var primeiraLetra = this.Nome[0].ToString();

            if (primeiraLetra != primeiraLetra.ToUpper().ToString())
            {
                yield return new ValidationResult("A primeira letra deve ser maiúscula",
                    new[] { nameof(this.Nome) });
            }
        }
    }
}
