using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models
{
    public class Produto //Classes anêmicas, SÓ temos propriedades
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [StringLength(80, ErrorMessage = "O nome deve ter até 20 caracteres")]
        [PrimeiraLetraMaiuscula]
        public string? Nome { get; set; }

        [Required]
        [StringLength(69, ErrorMessage = "A descrição deve ter no mínimo {1} caracteres")]
        public string? Descricao { get; set; }
        [Required]
        [Column(TypeName="decimal(10,2)")]

        [Range(1, 1000, ErrorMessage = "Preço deve estar entre 1 e 1000")]
        public decimal Preco { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }

        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
