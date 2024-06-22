using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject___Nicolas.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "Qual o seu nome?")]
        [MinLength(4, ErrorMessage = "Seu nome deve ter pelo menos quatro letras.")]
        [MaxLength(50, ErrorMessage = "")]
        public string? NomeCliente { get; set; }
        public string? EmailCliente { get; set; }
        public string? CPFCLiente { get; set; }
        public string? TelefoneCliente { get; set; }
        public ICollection<Pedido>? Pedidos { get; set; }
        public Endereco? Endereco { get; set; }
    }
}
