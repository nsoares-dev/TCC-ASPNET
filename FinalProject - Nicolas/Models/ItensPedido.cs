using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject___Nicolas.Models
{
    [Table("ItensPedido")]
    public class ItensPedido
    {
        public int ItensPedidoId { get; set; }
        public int Quantidade { get; set; }
        public int ProdutoId { get; set; }
        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; }
        public Produto? Produto { get; set; }
    }
}
