using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalProject___Nicolas.Models
{
    public class CategoriaProduto
    {
        public int CategoriaProdutoId { get; set; }
        [Required]
        [DisplayName("Nome")]
        public string? NomeCategoria { get; set; }
        [Required]
        [DisplayName("Descrição")]
        public string? DescricaoCategoria { get; set; }
        [Required]
        public bool Ativa {  get; set; }
        public ICollection<Produto>? Produtos { get; set; }
    }
}
