using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject___Nicolas.Models
{
    [Table("Produtos")]
    public class Produto
    {
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "Seu produto precisa ter um nome.")]
        [DisplayName("Nome")]
        [MaxLength(100)]
        public string? ProdutoNome { get; set; }
        [Required(ErrorMessage = "Qual o valor desse produto?")]
        [DisplayName("Preço")]
        public double ProdutoPreco { get; set; }
        [Required(ErrorMessage = "O quanto de estoque você tem disponível?")]
        [Range(1,999, ErrorMessage = "Seu estoque deve de produtos deve ser pelo menos de 1.")]
        public int Estoque { get; set; }
        [DisplayName("Disponível?")]
        public bool Disponivel { get; set; }
        public double? PrecoDesconto { get; set; }
        [Required(ErrorMessage = "Como é esse produto?")]
        public string? ImagemUrl { get; set; }
        [Required]
        public int CategoriaProdutoId { get; set; }
        public CategoriaProduto? CategoriaProduto { get; set; }

        [NotMapped]
        public IFormFile? Imagem { get; set; }
    }
}
