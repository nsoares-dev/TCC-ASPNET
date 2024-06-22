using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinalProject___Nicolas.Validators;

namespace FinalProject___Nicolas.Models
{
    [Table("Vendedor")]
    public class Vendedor
    {
        public int VendedorId { get; set; }
        [Required(ErrorMessage = "Qual o seu nome, vendedor?")]
        [MinLength(4, ErrorMessage = "Coloque um nome válido, por favor.")]
        public string? NomeVendedor { get; set; }
        [Required]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido.")]
        public string? EmailVendedor { get; set; }
        public string? TelefoneVendedor { get; set; }
        [Required(ErrorMessage = "Coloque um documento válido.")]
        [ValidatorCNPJCPF]
        [DisplayName("CNPJ/CPF")]
        public string? CPFCNPJVendedor { get; set; }
        public ICollection<Produto>? Produtos { get; set; }
    }
}
