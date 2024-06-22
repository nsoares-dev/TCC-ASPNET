using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject___Nicolas.Models
{
    [Table("Pedido")]
    public class Pedido
    {
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public ICollection<ItensPedido>? ItensPedido { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal Total { get; set; }
        public Endereco? EnderecoEntrega { get; set; }
        public int? EnderecoId { get; set; }
        public int StatusEntregaId { get; set; }
        public StatusEntrega? StatusEntrega { get; set; }
    }
}
