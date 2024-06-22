using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject___Nicolas.Models
{
    [Table("Endereco")]
    public class Endereco
    {
        public int EnderecoId { get; set; }
        public string? Logradouro { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? CEP { get; set; }
        public string? NumeroCasa { get; set; }
        public string? Complemento { get; set; }
        public int? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
